using BusinessLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public interface ILessonService
    {
        public Task<Lesson> CreateLesson(LessonDTO lessonDto);
        public void UpdateLesson(int lessonId, LessonDTO lessonDto);
        public void DeleteLesson(int lessonId);
        public Lesson_Course GetLessonById(int lessonId);
        public List<Lesson_Course> GetAllLessons();
    }
}
