using BusinessLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public interface ICourseService
    {
        public Task<CourseDto> CreateCourseAsync(CourseDto courseDto);
        public Task<CourseLessonsDto> GetCourseByIdAsync(int id);
        public Task UpdateCourseAsync(int id, CourseDto courseDto);
        public Task DeleteCourseAsync(int id);
        public Task<List<CourseLessonsDto>> GetAllCourseLessonsAsync();
    }

}
