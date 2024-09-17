using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
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
    }

    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IClassroomRepository _classroomRepository;

        public SpecializationService(ISpecializationRepository specializationRepository, IClassroomRepository classroomRepository)
        {
            _specializationRepository = specializationRepository;
            _classroomRepository = classroomRepository;
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
    }
}
