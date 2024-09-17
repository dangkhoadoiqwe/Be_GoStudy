using DataAccess.Model;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOStudy_Logic.Service
{
    public interface ISpecializationService
    {
        Task<bool> SaveSpecializationAsync(Specialization specialization);
    }
    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IClassroomRepository _classroomRepository;

        // Inject các repository vào service
        public SpecializationService(ISpecializationRepository specializationRepository, IClassroomRepository classroomRepository)
        {
            _specializationRepository = specializationRepository;
            _classroomRepository = classroomRepository;
        }

        // Phương thức để lưu Specialization và tạo Classroom
        public async Task<bool> SaveSpecializationAsync(Specialization specialization)
        {
            // Gọi repository để lưu specialization
            var result = await _specializationRepository.SaveSpecializationAsync(specialization);

            if (result)
            {   
                // Nếu lưu specialization thành công, tạo classroom mới
                var classroom = new Classroom
                {
                    Name = specialization.Name, // Sử dụng tên của specialization
                    SpecializationId = specialization.SpecializationId, // Gán specializationId vào classroom
                    CreatedAt = DateTime.UtcNow
                };

                // Lưu classroom bằng repository
                await _classroomRepository.AddClassroomAsync(classroom);
            }

            return result;
        }
    }
}
