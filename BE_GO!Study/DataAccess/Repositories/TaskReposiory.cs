using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Tasks>> GetTaskByID(int id);
        Task SaveTaskAsync(Tasks task);
    }
    public class TaskReposiory : ITaskRepository
    {
        private readonly GOStudyContext _studyContext;

        public TaskReposiory(GOStudyContext studyContext)
        {
            _studyContext = studyContext;
        }
        public async Task<IEnumerable<Tasks>> GetTaskByID(int id)
        {
            return await _studyContext.Tasks.Where(t => t.UserId == id).ToListAsync();
        }

        public async Task SaveTaskAsync(Tasks task)
        {
            _studyContext.Tasks.Add(task);  
            await _studyContext.SaveChangesAsync();
        }
    }
}
