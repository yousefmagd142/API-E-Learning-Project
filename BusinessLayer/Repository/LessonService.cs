using BusinessLayer.DTO;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.DTO;
using BusinessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace BusinessLayer.Repository
{

    public class LessonService : ILessonService
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        private readonly string _videopath;

        public LessonService(AppDbContext Db, IHostingEnvironment env)
        {
            _db = Db;
            _env = env;
            _videopath = $"{_env.WebRootPath}/uploads/videos/LessonVideos";
        }


        public async Task<Lesson> CreateLesson(LessonDTO lessonDto)
        {
            // Upload the video file and get the URL
            var videoname=$"{Guid.NewGuid()}{Path.GetExtension(lessonDto.VideoFile.FileName)}";
            var path = Path.Combine(_videopath,videoname);
            using var stream =File.Create(path);
            await lessonDto.VideoFile.CopyToAsync(stream);
            stream.Dispose();
            // Create a new Lesson object
            Lesson lesson = new Lesson
            {
                Content = lessonDto.Content,
                LessonTitle = lessonDto.LessonTitle,
                CourseID = lessonDto.CourseID, // Ensure this property exists in LessonDTO
                VideoURL = videoname
            };
            

            _db.Lessons.Add(lesson);
            _db.SaveChanges();
            return lesson;
        }


        public void DeleteLesson(int lessonId)
        {
            Lesson lesson = _db.Lessons.FirstOrDefault(o => o.Id == lessonId);
            if (lesson != null)
            {
                _db.Lessons.Remove(lesson);
                _db.SaveChangesAsync(); 
            }
        }

        public List<Lesson_Course> GetAllLessons()
        {

            var lessons = _db.Lessons.Include(o => o.Course).ToList();

            var lessonCourses = new List<Lesson_Course>();

            foreach (var lesson in lessons)
            {
                Lesson_Course lc = new Lesson_Course
                {
                    LessonTitle = lesson.LessonTitle,
                    Content = lesson.Content,
                    VideoURL = lesson.VideoURL,
                    CourseName = lesson.Course.CourseName
                };

                lessonCourses.Add(lc);
            }

            return lessonCourses;

        }


        public Lesson_Course GetLessonById(int lessonId)
        {

            var lesson = _db.Lessons.Include(o=>o.Course).FirstOrDefault(o => o.Id == lessonId);
            if (lesson != null) {
                Lesson_Course lc = new Lesson_Course();
                lc.LessonTitle = lesson.LessonTitle;
                lc.Content = lesson.Content;
                lc.VideoURL = lesson.VideoURL;
                lc.CourseName = lesson.Course.CourseName;
                return lc;
            }
            return null;

        }

        public void UpdateLesson(int lessonId, LessonDTO lessonDto)
        {
            Lesson lesson = _db.Lessons.FirstOrDefault(o => o.Id == lessonId);

            var videoname = $"{Guid.NewGuid()}{Path.GetExtension(lessonDto.VideoFile.FileName)}";
            var path = Path.Combine(_videopath, videoname);
            using var stream = File.Create(path);
            lessonDto.VideoFile.CopyTo(stream);
            stream.Dispose();

            lesson.LessonTitle = lessonDto.LessonTitle;
            lesson.Content   = lessonDto.Content; 
            lesson.CourseID = lessonDto.CourseID;
            lesson.VideoURL = videoname;
            _db.SaveChanges();
        }
    }
}