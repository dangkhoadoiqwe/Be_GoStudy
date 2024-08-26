using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IClassroomService
    {
        Task<IEnumerable<ClassroomModel>> GetAllClassroomsAsync();
        Task<ClassroomModel> GetClassroomByIdAsync(int classroomId);
        Task AddClassroomAsync(ClassroomModel classroomModel);
        Task UpdateClassroomAsync(ClassroomModel classroomModel);
        Task DeleteClassroomAsync(int classroomId);
        Task<IEnumerable<ClassroomModel>> GetUserRoomAsync(int userId);
        Task<IEnumerable<ClassroomModel>> GetOtherClassroomsAsync();
        Task<ClassUserModel> GetUserDashboardAsync(int userId);
    }

    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ClassroomService(
            IClassroomRepository repository,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
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

        public async Task AddClassroomAsync(ClassroomModel classroomModel)
        {
            var classroom = _mapper.Map<Classroom>(classroomModel);
            await _repository.AddClassroomAsync(classroom);
        }

        public async Task UpdateClassroomAsync(ClassroomModel classroomModel)
        {
            var classroom = _mapper.Map<Classroom>(classroomModel);
            await _repository.UpdateClassroomAsync(classroom);
        }

        public async Task DeleteClassroomAsync(int classroomId)
        {
            await _repository.DeleteClassroomAsync(classroomId);
        }

        public async Task<IEnumerable<ClassroomModel>> GetUserRoomAsync(int userId)
        {
            var userRooms = await _repository.GetUserRoomAsync(userId);
            return _mapper.Map<IEnumerable<ClassroomModel>>(userRooms);
        }

        public async Task<IEnumerable<ClassroomModel>> GetOtherClassroomsAsync()
        {
            var classrooms = await _repository.GetOtherClassroomsAsync();
            return _mapper.Map<IEnumerable<ClassroomModel>>(classrooms);
        }

        public async Task<ClassUserModel> GetUserDashboardAsync(int userId)
        {
            // Get friend requests
            var friendRequests = await _userRepository.GetAllFriendRequestsAsync(userId);

            // Get user rooms
            var userRooms = await _repository.GetUserRoomAsync(userId);

            // Get other classrooms
            var otherClassrooms = await _repository.GetOtherClassroomsAsync();

            // Map to ClassroomModel
            var userRoomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(userRooms);
            var otherClassroomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(otherClassrooms);

            // Create and return combined DTO
            return new ClassUserModel
            {
                FriendRequests = friendRequests,
                UserRooms = userRoomsMapped,
                OtherClassrooms = otherClassroomsMapped
            };
        }
    }
}
