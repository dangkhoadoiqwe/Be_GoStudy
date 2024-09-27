using DataAccess.Model;
using DataAccess.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IAttendanceService
    {
        Task AddAttendanceAsync(int userId, bool isPresent, string notes); // Thêm điểm danh
        Task<IEnumerable<Attendance>> GetAttendanceByUserIdAsync(int userId); // Lấy danh sách điểm danh của user
        Task SaveAttendanceAsync(int userId); // Lưu attendance (thêm mới hoặc cập nhật)
    }

    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        // Thêm điểm danh
        public async Task AddAttendanceAsync(int userId, bool isPresent, string notes)
        {
            var attendance = new Attendance
            {
                UserId = userId,
                Date = DateTime.UtcNow,
                IsPresent = isPresent,
                Notes = notes
            };

            await _attendanceRepository.AddAttendanceAsync(attendance);
        }

        // Lấy danh sách điểm danh của user
        public async Task<IEnumerable<Attendance>> GetAttendanceByUserIdAsync(int userId)
        {
            return await _attendanceRepository.GetAttendanceByUserIdAsync(userId);
        }

        // Lưu (thêm mới hoặc cập nhật) attendance
        public async Task SaveAttendanceAsync(int userId)
        {
            var attendance = new Attendance
            {
                UserId = userId,
                Date = DateTime.UtcNow, // Lưu theo ngày hiện tại
                IsPresent = true,
                Notes = "Attendance",
                
            };

            await _attendanceRepository.SaveAttendanceAsync(attendance);
        }
    }
}
