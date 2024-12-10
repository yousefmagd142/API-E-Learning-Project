using BusinessLayer.DTO;
using BusinessLayer.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elda7e7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProfileDataController : ControllerBase
    {
        private readonly IProfileDataService _Service;
        
        public ProfileDataController(IProfileDataService service)
        {
            _Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        { 
            var model = await _Service.GetProfileData();
            if (model == null) 
            {
                return NotFound();
            }
            return Ok(model);
        }
        [HttpPut]
        [Authorize (Roles ="Admin")]

        public async Task<IActionResult> updata(ProfileDataDto newprofiledata)
        {
            if (ModelState.IsValid)
            {
                await _Service.UpdateProfileData(newprofiledata);
                return NoContent();
            }
            else {return BadRequest(ModelState);}
        }
    }
}
