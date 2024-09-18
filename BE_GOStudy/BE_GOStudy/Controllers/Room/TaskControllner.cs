using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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

        // API to save a task
        [HttpPost("SaveTask")]
        public async Task<IActionResult> SaveTask([FromBody] TaskViewModel taskViewModel)
        {
            if (taskViewModel == null)
            {
                return BadRequest("Task is null.");
            }

            await _taskService.SaveTaskAsync(taskViewModel);

            return Ok("Task saved successfully.");
        }

        // API to get tasks for the current week
        [HttpGet("GetTasksForWeek/{userId}")]
        public async Task<IActionResult> GetTasksForWeek(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdForWeekAsync(userId);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user in the current week.");
            }

            return Ok(tasks);
        }

        // API to get tasks for the next week
        [HttpGet("GetTasksForNextWeek/{userId}")]
        public async Task<IActionResult> GetTasksForNextWeek(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdForNextWeekAsync(userId);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user in the next week.");
            }

            return Ok(tasks);
        }

        // API to get tasks for the previous week
        [HttpGet("GetTasksForPreviousWeek/{userId}")]
        public async Task<IActionResult> GetTasksForPreviousWeek(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdForPreviousWeekAsync(userId);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user in the previous week.");
            }

            return Ok(tasks);
        }

        // API to get tasks for the current month
        [HttpGet("GetTasksForMonth/{userId}")]
        public async Task<IActionResult> GetTasksForMonth(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdForMonthAsync(userId);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user in the current month.");
            }

            return Ok(tasks);
        }

        // API to get tasks for the next month
        [HttpGet("GetTasksForNextMonth/{userId}")]
        public async Task<IActionResult> GetTasksForNextMonth(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdForNextMonthAsync(userId);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user in the next month.");
            }

            return Ok(tasks);
        }

        // API to get tasks for the previous month
        [HttpGet("GetTasksForPreviousMonth/{userId}")]
        public async Task<IActionResult> GetTasksForPreviousMonth(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdForPreviousMonthAsync(userId);

            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found for this user in the previous month.");
            }

            return Ok(tasks);
        }
    }
}
