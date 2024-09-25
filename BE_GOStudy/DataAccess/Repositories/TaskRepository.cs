using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetTaskByUserIdForWeek(int userId);
        Task<IEnumerable<Tasks>> GetTaskByUserIdForNextWeek(int userId);
        Task<IEnumerable<Tasks>> GetTaskByUserIdForPreviousWeek(int userId);
        Task<IEnumerable<Tasks>> GetTaskByUserIdForMonth(int userId);
        Task<IEnumerable<Tasks>> GetTaskByUserIdForNextMonth(int userId);
        Task<IEnumerable<Tasks>> GetTaskByUserIdForPreviousMonth(int userId);
        Task SaveTaskAsync(Tasks task);
        Task<IEnumerable<Tasks>> GetTaskByID(int id);
        Task<IEnumerable<Tasks>> GetTaskByUserIdForToday(int userId);
    
        Task UpdateTask(Tasks task);
        

        Task<Tasks> GetTaskByTaskId(int TaskId);

    }

    public class TaskRepository : ITaskRepository
    {
        private readonly GOStudyContext _studyContext;

        public TaskRepository(GOStudyContext studyContext)
        {
            _studyContext = studyContext;
        }

        // Get tasks by user ID for the current week
        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForWeek(int userId)
        {
            // Get the current UTC date
            DateTime currentDate = DateTime.UtcNow;

            // Find the start of the current week (Monday)
            int diff = (7 + (currentDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = currentDate.AddDays(-diff).Date;

            // End of the current week (Sunday, 23:59:59)
            DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

            // Fetch tasks for the current week
            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfWeek &&
                            t.ScheduledTime <= endOfWeek)
                .ToListAsync();
        }
        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForToday(int userId)
        {
            // Get the current UTC date
            DateTime currentDate = DateTime.UtcNow.Date;

            // Define start and end of the current day
            DateTime startOfDay = currentDate;
            DateTime endOfDay = currentDate.AddDays(1).AddTicks(-1);

            // Fetch tasks for the current day
            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfDay &&
                            t.ScheduledTime <= endOfDay)
                .ToListAsync();
        }
        // Get tasks by user ID for the next week
        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForNextWeek(int userId)
        {
            // Get the current UTC date
            DateTime currentDate = DateTime.UtcNow;

            // Find the start of the next week (Monday)
            int diff = (7 + (currentDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfNextWeek = currentDate.AddDays(-diff + 7).Date;

            // End of the next week (Sunday, 23:59:59)
            DateTime endOfNextWeek = startOfNextWeek.AddDays(7).AddTicks(-1);

            // Fetch tasks for the next week
            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfNextWeek &&
                            t.ScheduledTime <= endOfNextWeek)
                .ToListAsync();
        }

        // Get tasks by user ID for the previous week
        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForPreviousWeek(int userId)
        {
            // Get the current UTC date
            DateTime currentDate = DateTime.UtcNow;

            // Find the start of the previous week (Monday)
            int diff = (7 + (currentDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfPreviousWeek = currentDate.AddDays(-diff - 7).Date;

            // End of the previous week (Sunday, 23:59:59)
            DateTime endOfPreviousWeek = startOfPreviousWeek.AddDays(7).AddTicks(-1);

            // Fetch tasks for the previous week
            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfPreviousWeek &&
                            t.ScheduledTime <= endOfPreviousWeek)
                .ToListAsync();
        }

        // Get tasks by user ID for the current month
        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForMonth(int userId)
        {
            // Get the current UTC date
            DateTime currentDate = DateTime.UtcNow;

            // Get the start of the current month (1st day of the month at 00:00:00)
            DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // Get the end of the current month (last day of the month at 23:59:59)
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

            // Fetch tasks for the current month
            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfMonth &&
                            t.ScheduledTime <= endOfMonth)
                .ToListAsync();
        }

        // Get tasks by user ID for the next month
        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForNextMonth(int userId)
        {
            // Get the current UTC date
            DateTime currentDate = DateTime.UtcNow;

            // Get the start of the next month (first day of the next month at 00:00:00)
            DateTime startOfNextMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);

            // Get the end of the next month (last day of the next month at 23:59:59)
            DateTime endOfNextMonth = startOfNextMonth.AddMonths(1).AddTicks(-1);

            // Fetch tasks for the next month
            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfNextMonth &&
                            t.ScheduledTime <= endOfNextMonth)
                .ToListAsync();
        }

        // Get tasks by user ID for the previous month
        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForPreviousMonth(int userId)
        {
            // Get the current UTC date
            DateTime currentDate = DateTime.UtcNow;

            // Get the start of the previous month (first day of the last month at 00:00:00)
            DateTime startOfPreviousMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-1);

            // Get the end of the previous month (last day of the last month at 23:59:59)
            DateTime endOfPreviousMonth = startOfPreviousMonth.AddMonths(1).AddTicks(-1);

            // Fetch tasks for the previous month
            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfPreviousMonth &&
                            t.ScheduledTime <= endOfPreviousMonth)
                .ToListAsync();
        }

        // Get task by ID
        public async Task<IEnumerable<Tasks>> GetTaskByID(int id)
        {
            return await _studyContext.Tasks.Where(t => t.UserId == id).ToListAsync();
        }

        // Save a new task
        public async Task SaveTaskAsync(Tasks task)
        {
            _studyContext.Tasks.Add(task);
            await _studyContext.SaveChangesAsync();
        }

        public async Task UpdateTask(Tasks task)
        {
            var exciting = await _studyContext.Users.FindAsync(task.UserId);
            if (exciting == null)
            {
                throw new Exception("User not found");
            }

            _studyContext.Tasks.Update(task);
            await _studyContext.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }
        


        public async Task<Tasks> GetTaskByTaskId(int TaskId)
        {
          return await _studyContext.Tasks.FirstOrDefaultAsync(t => t.TaskId == TaskId);
        }

        
    }
}
