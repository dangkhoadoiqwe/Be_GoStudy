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
        Task<IEnumerable<Classroom>> GetAllClassroomsAsync(int userId);
        Task<Classroom?> GetClassroomByIdAsync(int classroomId);
        Task AddClassroomAsync(Classroom classroom);
        Task UpdateClassroomAsync(Classroom classroom);
        Task DeleteClassroomAsync(int classroomId);
        Task<IEnumerable<Classroom>> GetOtherClassroomsAsync(int userId);
        Task<IEnumerable<Classroom>> GetUserRoomAsync( int userid );
    }
    public class ClassroomRepository : IClassroomRepository
    {
        private readonly GOStudyContext _context;

        public ClassroomRepository(GOStudyContext context)
        {
            _context = context;
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

        public async Task<IEnumerable<Classroom>> GetUserRoomAsync(int userId)
        {
            
            var specializationIds = await _context.UserSpecializations
                .Where(us => us.UserId == userId)
                .Select(us => us.SpecializationId)
                .Distinct()
                .ToListAsync();

            
            return await _context.Classrooms
                .Where(c => specializationIds.Contains(c.SpecializationId))
                .ToListAsync();
        }
      
        public async Task<IEnumerable<Classroom>> GetAllClassroomsAsync(int userId)
        {
            var userClassroomIds = await _context.UserSpecializations
                .Where(us => us.UserId == userId)
                .Select(us => us.SpecializationId)
                .Distinct()
                .ToListAsync();


            var otherClassrooms = await _context.Classrooms
                .Where(c => !userClassroomIds.Contains(c.ClassroomId))
                .Distinct()
                .ToListAsync();


            return otherClassrooms;
        }
        public async Task<IEnumerable<Classroom>> GetOtherClassroomsAsync(int userId)
        {
           
            var userClassroomIds = await _context.UserSpecializations
                .Where(us => us.UserId == userId)
                .Select(us => us.SpecializationId)
                .Distinct()
                .ToListAsync();


            var otherClassrooms = await _context.Classrooms
                .Where(c => !userClassroomIds.Contains(c.ClassroomId))   
                .Take(3)   
                .ToListAsync();

            return otherClassrooms;
        }

    }
}
