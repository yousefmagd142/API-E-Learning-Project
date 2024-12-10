using BusinessLayer.DTO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        private readonly string _videopath;

        public CourseService(AppDbContext Db, IHostingEnvironment env)
        {
            _db = Db;
            _env = env;
            _videopath = $"{_env.WebRootPath}/uploads/covers";
        }

        public async Task<CourseDto> CreateCourseAsync(CourseDto courseDto)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(courseDto.CourseCover.FileName)}";
            var path = Path.Combine(_videopath, covername);
            using var stream = File.Create(path);
            await courseDto.CourseCover.CopyToAsync(stream);
            stream.Dispose();

            Course course = new Course
            {
                CourseName = courseDto.CourseName,
                Description = courseDto.Description,
                CourseCover = covername
            };


            _db.Courses.Add(course);
            _db.SaveChanges();
            return courseDto;
        }

        public async Task DeleteCourseAsync(int id)
        {
            var cousrse = await _db.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (cousrse != null)
            {
                _db.Remove(cousrse);
                _db.SaveChanges();
            }
        }

        public async Task<List<CourseLessonsDto>> GetAllCourseLessonsAsync()
        {
            var courses = await _db.Courses.Include(c => c.Lessons).ToListAsync();

            var courseLessonDtos = new List<CourseLessonsDto>();

            foreach (var course in courses)
            {
                var courseLessonDto = new CourseLessonsDto
                {
                    CourseName = course.CourseName,
                    Description = course.Description,
                    CourseCoverUrl = course.CourseCover,
                    lessons = new List<lesson>() 
                };

                foreach (var lesson in course.Lessons)
                {
                    courseLessonDto.lessons.Add(new lesson
                    {
                        Content = lesson.Content,
                        LessonTitle = lesson.LessonTitle,
                        VideoUrl = lesson.VideoURL
                    });
                }

                courseLessonDtos.Add(courseLessonDto);
            }

            return courseLessonDtos;
        }




        public async Task<CourseLessonsDto> GetCourseByIdAsync(int id)
        {
            var course= await _db.Courses.Include(l=>l.Lessons).FirstOrDefaultAsync(o=>o.Id==id);

            CourseLessonsDto coursedto = new CourseLessonsDto();
            if (course != null)
            {
                coursedto.CourseName = course.CourseName;
                coursedto.Description = course.Description;
                coursedto.CourseCoverUrl = course.CourseCover;
                foreach (var l in course.Lessons)
                {
                    coursedto.lessons.Add(new lesson { Content = l.Content, LessonTitle = l.LessonTitle, VideoUrl = l.VideoURL });
                }
                return coursedto;
            }else return null;
        }

        public async Task UpdateCourseAsync(int id, CourseDto courseDto)
        {
            var course = await _db.Courses.FirstOrDefaultAsync(o => o.Id == id);
            
            course.CourseName = courseDto.CourseName; 
            course.Description = courseDto.Description;

            var coursecover = $"{Guid.NewGuid()}{Path.GetExtension(courseDto.CourseCover.FileName)}";
            var path = Path.Combine(_videopath, coursecover);
            using var stream = File.Create(path);
            courseDto.CourseCover.CopyTo(stream);
            stream.Dispose();

            course.CourseCover = coursecover;

            _db.SaveChanges();
        }
    }
}
