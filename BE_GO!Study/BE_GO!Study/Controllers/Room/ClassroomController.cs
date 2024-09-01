using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BE_GO_Study.Controllers.Room
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomService _service;

        public ClassroomController(IClassroomService service)
        {
            _service = service;
        }

        [HttpGet("AllClass/{userid}")]
        public async Task<IActionResult> GetAllClassrooms(int userid)
        {
            try
            {
                var classrooms = await _service.GetAllClassroomsAsync(userid);
                if (classrooms == null)
                {
                    return NotFound($"No classrooms found for user with ID {userid}");
                }
                return Ok(classrooms);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }




         
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassroom(int id, ClassroomModel ClassroomModel)
        {
            if (id != ClassroomModel.ClassroomId)
            {
                return BadRequest();
            }
            await _service.UpdateClassroomAsync(ClassroomModel);
            return NoContent();
        }

        [HttpGet("user/{userid}")]
        public async Task<ActionResult<IEnumerable<ClassroomModel>>> GetUserRooms(int userid)
        {
            var classrooms = await _service.GetUserDashboardAsync(userid);
            return Ok(classrooms);
        }

        
    }
}
