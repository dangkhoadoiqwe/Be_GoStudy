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
        public async Task SaveTaskAsync(TaskViewModel taskViewModel)
        {
            
            var taskEntity = _mapper.Map<Tasks>(taskViewModel);

           
            await _taskRepository.SaveTaskAsync(taskEntity);
        }
    }
}
