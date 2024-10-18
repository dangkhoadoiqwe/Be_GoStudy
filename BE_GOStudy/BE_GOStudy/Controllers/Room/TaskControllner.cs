using DataAccess.Repositories;
using GO_Study_Logic.Service;
using GO_Study_Logic.ViewModel;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpDelete("SoftDelete/{taskId}")]
        public async Task<IActionResult> SoftDeleteTask(int taskId)
        {
            try
            {
                var result = await _taskService.UpdateTaskDelete(taskId);
                if (result)
                {
                    return Ok("Task soft-deleted successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to soft delete the task.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("CompleteTask/{taskId}")]
        public async Task<IActionResult> CompleteTask(int taskId)
        {
            try
            {
                var result = await _taskService.UpdateTaskComplete(taskId);
                if (result)
                {
                    return Ok("Task completed successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to complete the task.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // API to save a task and add it to Google Calendar
        [HttpPost("SaveTask")]
        public async Task<IActionResult> SaveTask([FromBody] TaskViewModel taskViewModel)//, string googleAccessToken)
        {
            if (taskViewModel == null)
            {
                return BadRequest("Task is null.");
            }

            // Lưu task vào cơ sở dữ liệu SQL
            await _taskService.SaveTaskAsync(taskViewModel);

            // Lấy Access Token từ request thay vì từ cơ sở dữ liệu
           

            //if (string.IsNullOrEmpty(googleAccessToken))
            //{
            //    return BadRequest("Google Access Token not provided.");
            //}

            // Gọi Google Calendar API để thêm task vào Calendar
            //var addedToGoogleCalendar = await AddTaskToGoogleCalendar(googleAccessToken, taskViewModel);
            //if (!addedToGoogleCalendar)
            //{
            //    return StatusCode(500, "Failed to add task to Google Calendar.");
            //}

            return Ok("Task saved    successfully.");
        }

        // Hàm để thêm task vào Google Calendar
        //private async Task<bool> AddTaskToGoogleCalendar(string accessToken, TaskViewModel taskViewModel)
        //{
        //    try
        //    {
        //        var credential = GoogleCredential.FromAccessToken(accessToken);
        //        if (credential == null)
        //        {
        //            Console.WriteLine("Failed to create GoogleCredential from access token.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("GoogleCredential created successfully.");
        //        }

        //        var service = new CalendarService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = "GOStudy"
        //        });

        //        var calendarEvent = new Event
        //        {
        //            Summary = taskViewModel.Title,
        //            Description = taskViewModel.Description,
                     
        //            Start = new EventDateTime
        //            {
        //                DateTime = taskViewModel.ScheduledTime,
        //                TimeZone = "Asia/Ho_Chi_Minh",
        //            },
        //            End = new EventDateTime
        //            {
        //                DateTime = taskViewModel.ScheduledTime.Add(TimeSpan.FromMinutes(taskViewModel.TimeComplete)),
        //                TimeZone = "Asia/Ho_Chi_Minh",
        //            }
        //        };

        //        var request = service.Events.Insert(calendarEvent, "khoapdse161076@fpt.edu.vn");
        //        var createdEvent = await request.ExecuteAsync();

        //        return createdEvent != null; // Trả về true nếu tạo sự kiện thành công
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error adding to Google Calendar: {ex.Message}");
        //        return false;
        //    }
        //}

        [HttpGet("today/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskViewModel>>> GetTasksForToday(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdForTodayAsync(userId);
            return Ok(tasks);
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

        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskViewModel taskViewModel)
        {
            if (taskViewModel == null || taskViewModel.TaskId <= 0)
            {
                return BadRequest("Invalid task data.");
            }

            try
            {
                // Call the UpdateTask method from TaskService
                var result = await _taskService.UpdateTask(taskViewModel);

                if (result)
                {
                    return Ok("Task updated successfully.");
                }
                else
                {
                    return StatusCode(500, "Task update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
