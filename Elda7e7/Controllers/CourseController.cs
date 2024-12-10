using BusinessLayer.DTO;
using BusinessLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elda7e7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        public CourseController(ICourseService _Service)
        {
            courseService = _Service;
        }


        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateCourse(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdcourse = await courseService.CreateCourseAsync(courseDto);

            return Ok(createdcourse);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getcoursebyid(int id)
        {
            var model = await courseService.GetCourseByIdAsync(id);
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult>GetAllCourses()
        {
            var model = await courseService.GetAllCourseLessonsAsync();
            if (model != null) { return Ok(model); }
            else return NotFound();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, CourseDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await courseService.UpdateCourseAsync(id, courseDto);
            return StatusCode(204);//created put no content returns

        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {  
            var course = await courseService.GetCourseByIdAsync(id);
            if(course != null)
            {
                await courseService.DeleteCourseAsync(id);
                return NoContent();
            }
            return NotFound();
        }
    }
}
