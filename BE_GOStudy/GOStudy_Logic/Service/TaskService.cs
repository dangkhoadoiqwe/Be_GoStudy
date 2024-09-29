using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
 
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface ITaskService
    {
        Task SaveTaskAsync(TaskViewModel taskViewModel);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForWeekAsync(int userId);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForNextWeekAsync(int userId);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForPreviousWeekAsync(int userId);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForMonthAsync(int userId);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForNextMonthAsync(int userId);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForPreviousMonthAsync(int userId);
        Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForTodayAsync(int userId);

       // Task<bool> UpdateStatusTaskCompleteAsync(int id);
      //  Task<bool> DeletTask(int taskid);
      Task<bool> UpdateTaskDelete(int  taskid);

        Task<bool> UpdateTaskComplete(int taskid);
        Task<bool> UpdateTask(TaskViewModel taskViewModel);

      //  Task AddTaskAsync(TaskViewModel taskViewModel);
    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
      

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
           
        }


        // Get tasks for the current week
        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForWeekAsync(int userId)
        {
            var tasks = await _taskRepository.GetTaskByUserIdForWeek(userId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }
        
        // Get tasks for the next week
        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForNextWeekAsync(int userId)
        {
            var tasks = await _taskRepository.GetTaskByUserIdForNextWeek(userId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }
        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForTodayAsync(int userId)
        {
            var tasks = await _taskRepository.GetTaskByUserIdForToday(userId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }

        // Get tasks for the previous week
        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForPreviousWeekAsync(int userId)
        {
            var tasks = await _taskRepository.GetTaskByUserIdForPreviousWeek(userId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }

        // Get tasks for the current month
        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForMonthAsync(int userId)
        {
            var tasks = await _taskRepository.GetTaskByUserIdForMonth(userId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }

        // Get tasks for the next month
        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForNextMonthAsync(int userId)
        {
            var tasks = await _taskRepository.GetTaskByUserIdForNextMonth(userId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }

        // Get tasks for the previous month
        public async Task<IEnumerable<TaskViewModel>> GetTasksByUserIdForPreviousMonthAsync(int userId)
        {
            var tasks = await _taskRepository.GetTaskByUserIdForPreviousMonth(userId);
            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }

        // Save a new task
        public async Task SaveTaskAsync(TaskViewModel taskViewModel)
        {
            
            var taskEntity = _mapper.Map<Tasks>(taskViewModel);
            taskEntity.Status = false;
            taskEntity.IsDeleted = false;
            // Save the task entity
            await _taskRepository.SaveTaskAsync(taskEntity);
        }
        public async Task<bool> UpdateStatusTaskCompleteAsync(int id)
        {
            var task = await _taskRepository.GetTaskByTaskId(id);
            if (task == null)
            {
                throw new Exception("Task not found");
            }
            task.Status = true;
            await _taskRepository.UpdateTask(task);
            return true;
        }
        public async Task<bool> UpdateTask(TaskViewModel taskViewModel)
        {
            try
            {
                var task = await _taskRepository.GetTaskByTaskId(taskViewModel.TaskId);
                if (task == null)
                {
                    throw new Exception("Task not found"); 
                }
                task.Title = !string.IsNullOrEmpty(taskViewModel.Title) && taskViewModel.Title != "string" ? taskViewModel.Title : task.Title;
                task.Description = !string.IsNullOrEmpty(taskViewModel.Description) && taskViewModel.Description != "string" ? taskViewModel.Description : task.Description;

                task.ScheduledTime = taskViewModel.ScheduledTime != DateTime.MinValue
                                           ? taskViewModel.ScheduledTime
                                               : task.ScheduledTime;



                await _taskRepository.UpdateTask(task);
                return true;
            }
            catch (Exception ex)
            {
                throw ;
 
            }
        }
         

              public async Task<bool> UpdateTaskComplete(int taskId)
        {
            try
            {
                // Tìm task theo taskId
                var task = await _taskRepository.GetTaskByTaskId(taskId);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }

                // Cập nhật IsDeleted thành true (1)
                task.Status = true;

                // Gọi phương thức cập nhật task trong repository để lưu thay đổi
                await _taskRepository.UpdateTask(task);

                return true;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần thiết
                throw new Exception($"An error occurred while deleting the task: {ex.Message}");
            }
        }

        public async Task<bool> UpdateTaskDelete(int taskId)
        {
            try
            {
                // Tìm task theo taskId
                var task = await _taskRepository.GetTaskByTaskId(taskId);
                if (task == null)
                {
                    throw new Exception("Task not found");
                }

                // Cập nhật IsDeleted thành true (1)
                task.IsDeleted = true;

                // Gọi phương thức cập nhật task trong repository để lưu thay đổi
                await _taskRepository.UpdateTask(task);

                return true;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần thiết
                throw new Exception($"An error occurred while deleting the task: {ex.Message}");
            }
        }

    }
}
