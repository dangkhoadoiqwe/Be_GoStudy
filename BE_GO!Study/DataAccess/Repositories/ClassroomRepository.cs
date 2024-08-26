using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IClassroomRepository
    {
        Task<IEnumerable<Classroom>> GetAllClassroomsAsync();
        Task<Classroom?> GetClassroomByIdAsync(int classroomId);
        Task AddClassroomAsync(Classroom classroom);
        Task UpdateClassroomAsync(Classroom classroom);
        Task DeleteClassroomAsync(int classroomId);
        Task<IEnumerable<Classroom>> GetOtherClassroomsAsync();
        Task<IEnumerable<Classroom>> GetUserRoomAsync( int userid );
    }
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly GOStudyContext _context;

        public ClassroomRepository(GOStudyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Classroom>> GetAllClassroomsAsync()
        {
            return await _context.Classrooms.ToListAsync();
        }

        public async Task<Classroom?> GetClassroomByIdAsync(int classroomId)
        {
            return await _context.Classrooms.FindAsync(classroomId);
        }

        public async Task AddClassroomAsync(Classroom classroom)
        {
            await _context.Classrooms.AddAsync(classroom);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClassroomAsync(Classroom classroom)
        {
            _context.Classrooms.Update(classroom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClassroomAsync(int classroomId)
        {
            var classroom = await _context.Classrooms.FindAsync(classroomId);
            if (classroom != null)
            {
                _context.Classrooms.Remove(classroom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Classroom>> GetUserRoomAsync(int userid)
        {
            
            var classrooms = await _context.Analytics
                .Where(a => a.UserId == userid)           
                .Select(a => a.ClassroomId)               
                .Distinct()                              
                .ToListAsync();                          
 
            return await _context.Classrooms
                .Where(c => classrooms.Contains(c.ClassroomId))   
                .ToListAsync();                                    
        }

        public async Task<IEnumerable<Classroom>> GetOtherClassroomsAsync()
        {
            return await _context.Set<Classroom>().Take(3).ToListAsync();
        }
    }
}
