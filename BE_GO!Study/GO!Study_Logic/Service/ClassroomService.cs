using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IClassroomService
    {
        Task<IEnumerable<ClassroomModel>> GetAllClassroomsAsync();
        Task<ClassroomModel> GetClassroomByIdAsync(int classroomId);
        Task AddClassroomAsync(ClassroomModel ClassroomModel);
        Task UpdateClassroomAsync(ClassroomModel ClassroomModel);
        Task DeleteClassroomAsync(int classroomId);
    }
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _repository;
        private readonly IMapper _mapper;

        public ClassroomService(IClassroomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassroomModel>> GetAllClassroomsAsync()
        {
            var classrooms = await _repository.GetAllClassroomsAsync();
            return _mapper.Map<IEnumerable<ClassroomModel>>(classrooms);
        }

        public async Task<ClassroomModel> GetClassroomByIdAsync(int classroomId)
        {
            var classroom = await _repository.GetClassroomByIdAsync(classroomId);
            return _mapper.Map<ClassroomModel>(classroom);
        }

        public async Task AddClassroomAsync(ClassroomModel ClassroomModel)
        {
            var classroom = _mapper.Map<Classroom>(ClassroomModel);
            await _repository.AddClassroomAsync(classroom);
        }

        public async Task UpdateClassroomAsync(ClassroomModel ClassroomModel)
        {
            var classroom = _mapper.Map<Classroom>(ClassroomModel);
            await _repository.UpdateClassroomAsync(classroom);
        }

        public async Task DeleteClassroomAsync(int classroomId)
        {
            await _repository.DeleteClassroomAsync(classroomId);
        }
    }
}
