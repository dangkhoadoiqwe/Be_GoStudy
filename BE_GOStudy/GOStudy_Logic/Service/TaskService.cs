using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
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
            // Convert from TaskViewModel to Tasks entity
            var taskEntity = _mapper.Map<Tasks>(taskViewModel);

            // Save the task entity
            await _taskRepository.SaveTaskAsync(taskEntity);
        }
    }
}
