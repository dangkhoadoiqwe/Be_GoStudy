using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BE_GO_Study.Controllers
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassroomModel>>> GetAllClassrooms()
        {
            var classrooms = await _service.GetAllClassroomsAsync();
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassroomModel>> GetClassroomById(int id)
        {
            var classroom = await _service.GetClassroomByIdAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }
            return Ok(classroom);
        }

        [HttpPost]
        public async Task<IActionResult> AddClassroom(ClassroomModel ClassroomModel)
        {
            await _service.AddClassroomAsync(ClassroomModel);
            return CreatedAtAction(nameof(GetClassroomById), new { id = ClassroomModel.ClassroomId }, ClassroomModel);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            await _service.DeleteClassroomAsync(id);
            return NoContent();
        }
    }
}
