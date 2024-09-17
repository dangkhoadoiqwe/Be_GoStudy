using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IAttendanceRepository
    {
        Task AddAttendanceAsync(Attendance attendance); // Thêm mới attendance
        Task<IEnumerable<Attendance>> GetAttendanceByUserIdAsync(int userId); // Lấy danh sách điểm danh theo userId
        Task SaveAttendanceAsync(Attendance attendance); // Lưu (cập nhật) attendance
    }

    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly GOStudyContext _context;

        public AttendanceRepository(GOStudyContext context)
        {
            _context = context;
        }

        // Thêm mới điểm danh
        public async Task AddAttendanceAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);
            await _context.SaveChangesAsync();
        }

        // Lấy danh sách điểm danh theo userId
        public async Task<IEnumerable<Attendance>> GetAttendanceByUserIdAsync(int userId)
        {
            return await _context.Attendances
                .Where(a => a.UserId == userId)
                .ToListAsync();
        }

        // Lưu (cập nhật) attendance
        public async Task SaveAttendanceAsync(Attendance attendance)
        {
            var existingAttendance = await _context.Attendances
                .FirstOrDefaultAsync(a => a.UserId == attendance.UserId && a.Date.Date == attendance.Date.Date);

            if (existingAttendance != null)
            {
                // Cập nhật thông tin attendance hiện tại
                existingAttendance.IsPresent = attendance.IsPresent;
                existingAttendance.Notes = attendance.Notes;
            }
            else
            {
                // Nếu không tồn tại, thêm mới attendance
                await _context.Attendances.AddAsync(attendance);
            }

            await _context.SaveChangesAsync();
        }
    }
}
