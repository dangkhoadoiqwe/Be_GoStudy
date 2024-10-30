using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
using GOStudy_Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface ISpecializationService
    {
        Task<bool> SaveSpecializationAsync(SpecializationViewModel specialization);
        Task<IEnumerable<SpecializationViewModel>> GetAllSpecializationAsync();
        Task<bool> SaveSpecializationsForUserAsync(int userId, IEnumerable<int> specializationViewModels);
        Task<IEnumerable<SpecializationViewModelByUser>> GetallspbyUserid(int userId);
        Task<bool> UpdateSpecializationForUsersAsync(int userId, int specializationId);
        Task<bool> UpdateSpecializationForUserAsync(int userSpecializationId, int specializationId);
        Task<IEnumerable<SpecializationViewModel>> GetAvailableSpecializationsForUserAsync(int userId);
    }

    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserRepository userRepository;
        public SpecializationService(ISpecializationRepository specializationRepository,
            IClassroomRepository classroomRepository,
            IUserRepository userRepository)
        {
            _specializationRepository = specializationRepository;
            _classroomRepository = classroomRepository;
            this.userRepository = userRepository;
        }
        public async Task<bool> UpdateSpecializationForUsersAsync(int userId, int specializationId)
        {
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false; // User not found
            }

            var dateStart = DateTime.UtcNow;
            TimeSpan duration = user.Role switch
            {
                1 => TimeSpan.FromDays(30),  // Role 1: Add 1 month
                3 => TimeSpan.FromDays(7),   // Role 3: Add 1 week
                4 => TimeSpan.FromDays(1),   // Role 4: Add 1 day
                _ => TimeSpan.Zero
            };

            if (duration == TimeSpan.Zero)
            {
                return false; // Invalid role
            }

            var dateEnd = dateStart.Add(duration);
            return await _specializationRepository.UpdateUserSpecializationAsync(userId, specializationId, dateStart, dateEnd);
        }
        public async Task<bool> UpdateSpecializationForUserAsync(int userSpecializationId, int specializationId)
        {
            return await _specializationRepository.UpdateUserSpecializationAsync(userSpecializationId, specializationId);
        }
    
    public async Task<IEnumerable<SpecializationViewModelByUser>> GetallspbyUserid(int userId)
        {
            var userbyid = await userRepository.GetByIdAsync(userId);
            if (userbyid == null)
            {
                // Trả về danh sách rỗng nếu không tìm thấy user
                return new List<SpecializationViewModelByUser>();
            }

            // Lấy tất cả các chuyên ngành của người dùng từ repository
            var specializations = await _specializationRepository.GetAllSpecializationsByUserIDAsync(userId);

            // Ánh xạ từ entity sang view model
            var result = specializations.Select(s => new SpecializationViewModelByUser
            {
                SpecializationId = s.SpecializationId,
                Name = s.Name,
                user = new UserViewModel
                {
                    UserId = userbyid.UserId,
                    FullName = userbyid.FullName,
                    Email = userbyid.Email,
                    Role = userbyid.Role
                }
            });

            return result;
        }

        public async Task<IEnumerable<SpecializationViewModel>> GetAllSpecializationAsync()
        {
            // Fetch all Specializations from the repository
            var specializations = await _specializationRepository.GetAllAsync();

            return specializations.Select(specialization => new SpecializationViewModel
            {
                SpecializationId = specialization.SpecializationId,
                Name = specialization.Name
            }).ToList();
        }

        public async Task<bool> SaveSpecializationAsync(SpecializationViewModel specializationViewModel)
        {
            // Chuyển đổi từ ViewModel sang Entity
            var specialization = new Specialization
            {
                SpecializationId = specializationViewModel.SpecializationId,
                Name = specializationViewModel.Name
            };

            // Lưu specialization vào repository
            var result = await _specializationRepository.SaveSpecializationAsync(specialization);

            if (result)
            {
                // Nếu lưu specialization thành công, tạo classroom mới
                var classroom = new Classroom
                {
                    Name = specialization.Name,
                    SpecializationId = specialization.SpecializationId,
                    CreatedAt = DateTime.UtcNow,
                    LinkUrl = ".",
                    Nickname= specialization.Name,
                };

                await _classroomRepository.AddClassroomAsync(classroom);
            }

            return result;
        }
        public async Task<IEnumerable<SpecializationViewModel>> GetAvailableSpecializationsForUserAsync(int userId)
        {
            var specializations = await _specializationRepository.GetAvailableSpecializationsForUserAsync(userId);

            return specializations.Select(s => new SpecializationViewModel
            {
                SpecializationId = s.SpecializationId,
                Name = s.Name
            }).ToList();
        }
        public async Task<bool> SaveSpecializationsForUserAsync(int userId, IEnumerable<int> specializationIds)
        {
            var success = true;
            var dateStart = DateTime.UtcNow;

            // Retrieve user information to determine their role
            var user = await userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false; // User not found, cannot proceed
            }

            // Set the DateEnd based on the user's role
            TimeSpan duration = user.Role switch
            {
                1 => TimeSpan.FromDays(30),    // Role 1: add 1 month
                3 => TimeSpan.FromDays(7),     // Role 3: add 1 week
                4 => TimeSpan.FromDays(1),     // Role 4: add 1 day
                _ => TimeSpan.Zero             // Invalid role, no duration
            };

            if (duration == TimeSpan.Zero)
            {
                return false; // Invalid role specified
            }

            // Filter out any invalid IDs (though they should have been validated in the controller)
            var validSpecializationIds = specializationIds.Where(id => id > 0).ToList();

            if (validSpecializationIds.Any())
            {
                foreach (var specializationId in validSpecializationIds)
                {
                    // Create a new UserSpecialization object with DateEnd calculated based on role
                    var userSpecialization = new UserSpecialization
                    {
                        UserId = userId,
                        SpecializationId = specializationId,
                        DateStart = dateStart,
                        DateEnd = dateStart.Add(duration) // Set DateEnd based on the role-specific duration
                    };

                    // Save the UserSpecialization to the repository
                    var userSpecializationSaveResult = await _specializationRepository.SaveUserSpecializationAsync(userSpecialization);
                    success &= userSpecializationSaveResult;
                }

                return success;
            }

            return false;
        }

    }
}
