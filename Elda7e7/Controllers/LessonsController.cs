using BusinessLayer.DTO;
using BusinessLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Elda7e7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService services;

        public LessonsController(ILessonService _services)
        {
            services = _services;
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetLessonById(int id)
        {
            var lesson =  services.GetLessonById(id); 
            if (lesson == null)
            {
                return BadRequest("There is no lesson like that.");
            }
            return Ok(lesson);
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllLessons() 
        {
            var lessons= services.GetAllLessons();
            if (lessons == null)
            {
                return BadRequest();
                    }
            return Ok(lessons);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateLesson( LessonDTO lessonDto)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLesson = await services.CreateLesson(lessonDto);
            return CreatedAtAction(nameof(GetLessonById), new { id = createdLesson.Id }, createdLesson);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update(int Id,LessonDTO updatedlesson) 
        {
            if (!ModelState.IsValid)
            {  
                return BadRequest(ModelState); 
            }

            services.UpdateLesson(Id,updatedlesson);
            return StatusCode(204);//created put no content returns
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            var lesson = services.GetLessonById(id);    
            if(lesson != null) 
            {
                services.DeleteLesson(id);
                return NoContent();
            }
            return NotFound();  
        
        }

    }

}

