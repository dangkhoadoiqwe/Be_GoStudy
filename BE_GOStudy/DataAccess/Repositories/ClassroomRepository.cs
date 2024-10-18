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
        Task UpdateClassroomLinkUrlAsync(int classroomId, string linkUrl);
        Task DeleteClassroomAsync(int classroomId);
        Task<IEnumerable<Classroom>> GetOtherClassroomsAsync(int userId);
        Task<IEnumerable<Classroom>> GetUserRoomAsync( int userid );

        Task<IEnumerable<Classroom>> GetAllClassrooms();
        Task UpdateClassroomLinkYtbUrlAsync(int classroomId, string linkUrl);
        Task<bool> CheckRoomUser(int userid , int roomId);
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

        public async Task UpdateClassroomLinkUrlAsync(int classroomId, string linkUrl)
        {
            var classroom = await _context.Classrooms.FindAsync(classroomId);
            if (classroom != null)
            {
                classroom.LinkUrl = linkUrl; // Cập nhật LinkUrl
                _context.Classrooms.Attach(classroom).Property(c => c.LinkUrl).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Classroom>> GetAllClassrooms()
        {
            return await _context.Classrooms.ToListAsync();
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
            // Lấy danh sách ID của các lớp học mà người dùng đã tham gia
            var userClassroomIds = await _context.UserSpecializations
                .Where(us => us.UserId == userId)
                .Select(us => us.SpecializationId)
                .Distinct()
                .ToListAsync();

            // Lấy danh sách các lớp học mà người dùng đã tham gia
            var userClassrooms = await _context.Classrooms
                .Where(c => userClassroomIds.Contains(c.SpecializationId)) // Lưu ý là so sánh với SpecializationId
                .ToListAsync();

            // Lấy danh sách các lớp học khác mà người dùng chưa tham gia
            var otherClassrooms = await _context.Classrooms
                .Where(c => !userClassroomIds.Contains(c.SpecializationId)) // Lưu ý là so sánh với SpecializationId
                .Take(3) // Giới hạn số lượng lớp học khác trả về
                .ToListAsync();

            // Kết hợp danh sách lớp học của người dùng và lớp học khác
            return userClassrooms.Concat(otherClassrooms);
        }


        public async Task<IEnumerable<Classroom>> GetOtherClassroomsAsync(int userId)
        {
            // Lấy danh sách các SpecializationId mà người dùng đã tham gia
            var userSpecializationIds = await _context.UserSpecializations
                .Where(us => us.UserId == userId)
                .Select(us => us.SpecializationId)
                .Distinct()
                .ToListAsync();

            // Lấy danh sách các ClassroomId dựa trên SpecializationId mà người dùng đã tham gia
            var userClassroomIds = await _context.Classrooms
                .Where(c => userSpecializationIds.Contains(c.SpecializationId))
                .Select(c => c.ClassroomId)
                .Distinct()
                .ToListAsync();

            // Lấy các lớp học khác mà người dùng chưa tham gia
            var otherClassrooms = await _context.Classrooms
                .Where(c => !userClassroomIds.Contains(c.ClassroomId))
                .Distinct() // Bỏ qua các bản sao
                .ToListAsync();

            return otherClassrooms;
        }



        public async Task<bool> CheckRoomUser(int userId, int roomId)
        {
            // Get the specialization ID associated with the given roomId (Classroom)
            var specializationId = await _context.Classrooms
                .Where(c => c.ClassroomId == roomId)
                .Select(c => c.SpecializationId)
                .FirstOrDefaultAsync();

            // If no specialization is found for the room, return false
            if (specializationId == 0)
            {
                return false;
            }

            // Check if the user has this specialization in their UserSpecializations
            var userHasSpecialization = await _context.UserSpecializations
                .AnyAsync(us => us.UserId == userId && us.SpecializationId == specializationId);

            return userHasSpecialization;
        }
        
        public async Task UpdateClassroomLinkYtbUrlAsync(int classroomId, string linkUrl)
        {
            var classroom = await _context.Classrooms.FindAsync(classroomId);
            if (classroom != null)
            {
                classroom.YoutubeUrl = linkUrl; // Cập nhật LinkUrl
                _context.Classrooms.Attach(classroom).Property(c => c.YoutubeUrl).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
