using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BE_GOStudy.Controllers.Room
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("SaveTakUserID/{userid}")]
        public async Task<IActionResult> SaveTask([FromBody] TaskViewModel taskViewModel)
        {
            if (taskViewModel == null)
            {
                return BadRequest("Task is null.");
            }

            await _taskService.SaveTaskAsync(taskViewModel);

            return Ok("Task saved successfully.");
        }
    }
}
