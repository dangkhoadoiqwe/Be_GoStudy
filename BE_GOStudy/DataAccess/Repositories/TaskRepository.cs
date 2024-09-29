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
            DateTime currentDate = DateTime.UtcNow;
            int diff = (7 + (currentDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = currentDate.AddDays(-diff).Date;
            DateTime endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfWeek &&
                            t.ScheduledTime <= endOfWeek &&
                            !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForToday(int userId)
        {
            DateTime currentDate = DateTime.UtcNow.Date;
            DateTime startOfDay = currentDate;
            DateTime endOfDay = currentDate.AddDays(1).AddTicks(-1);

            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfDay &&
                            t.ScheduledTime <= endOfDay &&
                            !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForNextWeek(int userId)
        {
            DateTime currentDate = DateTime.UtcNow;
            int diff = (7 + (currentDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfNextWeek = currentDate.AddDays(-diff + 7).Date;
            DateTime endOfNextWeek = startOfNextWeek.AddDays(7).AddTicks(-1);

            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfNextWeek &&
                            t.ScheduledTime <= endOfNextWeek &&
                            !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForPreviousWeek(int userId)
        {
            DateTime currentDate = DateTime.UtcNow;
            int diff = (7 + (currentDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfPreviousWeek = currentDate.AddDays(-diff - 7).Date;
            DateTime endOfPreviousWeek = startOfPreviousWeek.AddDays(7).AddTicks(-1);

            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfPreviousWeek &&
                            t.ScheduledTime <= endOfPreviousWeek &&
                            !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForMonth(int userId)
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddTicks(-1);

            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfMonth &&
                            t.ScheduledTime <= endOfMonth &&
                            !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForNextMonth(int userId)
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime startOfNextMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(1);
            DateTime endOfNextMonth = startOfNextMonth.AddMonths(1).AddTicks(-1);

            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfNextMonth &&
                            t.ScheduledTime <= endOfNextMonth &&
                            !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTaskByUserIdForPreviousMonth(int userId)
        {
            DateTime currentDate = DateTime.UtcNow;
            DateTime startOfPreviousMonth = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-1);
            DateTime endOfPreviousMonth = startOfPreviousMonth.AddMonths(1).AddTicks(-1);

            return await _studyContext.Tasks
                .Where(t => t.UserId == userId &&
                            t.ScheduledTime >= startOfPreviousMonth &&
                            t.ScheduledTime <= endOfPreviousMonth &&
                            !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTaskByID(int id)
        {
            return await _studyContext.Tasks
                .Where(t => t.UserId == id && !t.IsDeleted) // Điều kiện thêm vào
                .ToListAsync();
        }

        public async Task SaveTaskAsync(Tasks task)
        {
            _studyContext.Tasks.Add(task);
            await _studyContext.SaveChangesAsync();
        }

        public async Task UpdateTask(Tasks task)
        {
            var existing = await _studyContext.Users.FindAsync(task.UserId);
            if (existing == null)
            {
                throw new Exception("User not found");
            }

            _studyContext.Tasks.Update(task);
            await _studyContext.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }
        // Get tasks by user ID for the current month
        

        public async Task<Tasks> GetTaskByTaskId(int TaskId)
        {
            return await _studyContext.Tasks.FirstOrDefaultAsync(t => t.TaskId == TaskId && !t.IsDeleted); // Điều kiện thêm vào
        }
    }

}
