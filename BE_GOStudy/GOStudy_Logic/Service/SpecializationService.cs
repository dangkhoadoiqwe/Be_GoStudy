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

        public async Task<IEnumerable<SpecializationViewModelByUser>> GetallspbyUserid(int userId)
        {
            var userbyid = await userRepository.GetByIdAsync(userId);
            if (userbyid == null)
            {
                // Return an empty list or throw an exception if the user does not exist
                return new List<SpecializationViewModelByUser>();
            }
            var spe = await _specializationRepository.GetAllSpecializationsByUserIDAsync(userId);
            var rs = spe.Select(s => new SpecializationViewModelByUser
            {
                SpecializationId = s.SpecializationId,
                Name = s.Name,
                user = new UserViewModel
                {
                    UserId = userbyid.UserId,
                    FullName = userbyid.FullName,
                    Email = userbyid.Email,
                    Role = userbyid.Role,
                    PasswordHash = userbyid.PasswordHash // Although it's usually not a good practice to expose PasswordHash in the ViewModel
                }
            });
            return rs;
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

        public async Task<bool> SaveSpecializationsForUserAsync(int userId, IEnumerable<int> specializationIds)
        {
            var success = true;
            var dateStart = DateTime.UtcNow;
            // Filter out any invalid IDs (though they should have been validated in the controller)
            var validSpecializationIds = specializationIds.Where(id => id > 0).ToList();

            if (validSpecializationIds.Any())
            {
                foreach (var specializationId in validSpecializationIds)
                {
                    // Create a new UserSpecialization object
                    var userSpecialization = new UserSpecialization
                    {
                        UserId = userId,
                        SpecializationId = specializationId,
                        DateStart = dateStart,
                        DateEnd = dateStart.AddMonths(4) // assuming no end date yet
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
