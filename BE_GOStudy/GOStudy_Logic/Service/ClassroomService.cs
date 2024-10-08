﻿using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.AutoMapperModule;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IClassroomService
    {
        Task<AllClassModel> GetAllClassroomsAsync(int userid);
        Task<ClassroomModel> GetClassroomByIdAsync(int classroomId);
        Task AddClassroomAsync(ClassroomModel classroomModel);
        Task UpdateClassroomLinkUrlAsync(int classroomId, string linkUrl);
        Task DeleteClassroomAsync(int classroomId);
        Task<IEnumerable<ClassroomModel>> GetUserRoomAsync(int userId);
        Task<IEnumerable<ClassroomModel>> GetOtherClassroomsAsync(int userId);
        Task<ClassUserModel> GetUserDashboardAsync(int userId);

        Task<IEnumerable<TaskViewMeeting>>GetRoomByUserID(int userId);

        Task<IEnumerable<ClassroomNameModel>> GetAllClassrooms();
    }

    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;

        public ClassroomService(
            IClassroomRepository repository,
            IMapper mapper,
            IUserRepository userRepository,
            ITaskRepository taskRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
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

        public async Task UpdateClassroomLinkUrlAsync(int classroomId, string linkUrl)
        {
            await _repository.UpdateClassroomLinkUrlAsync(classroomId, linkUrl);
        }
        public async Task<IEnumerable<ClassroomNameModel>> GetAllClassrooms()
        {
            var classrooms = await _repository.GetAllClassrooms();
            return _mapper.Map<IEnumerable<ClassroomNameModel>>(classrooms);
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

        public async Task<IEnumerable<ClassroomModel>> GetOtherClassroomsAsync(int userid)
        {
            var classrooms = await _repository.GetOtherClassroomsAsync(userid);
            return _mapper.Map<IEnumerable<ClassroomModel>>(classrooms);
        }
        public async Task<AllClassModel> GetAllClassroomsAsync(int userid)
        {
            var classrooms = await _repository.GetAllClassroomsAsync(userid);
            var userDashboard = await _repository.GetUserRoomAsync(userid);

            var userRoomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(userDashboard);
            var otherClassroomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(classrooms);

            return new AllClassModel
            {
                ClassUser = userRoomsMapped,
                Classroom = otherClassroomsMapped
            };
        }

        public async Task<ClassUserModel> GetUserDashboardAsync(int userId)
        {
            var friendRequests = await _userRepository.GetAllFriendRequestsAsync(userId);
            var userdetail = await _userRepository.GetByIdAsync(userId);
            var userRooms = await _repository.GetUserRoomAsync(userId);
            var otherClassrooms = await _repository.GetOtherClassroomsAsync(userId);

            var userdetailMapped = _mapper.Map<UserViewModel>(userdetail);
            var friendRequestsMapped = _mapper.Map<IEnumerable<FriendRequest_View_Model>>(friendRequests);
            var userRoomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(userRooms);
            var otherClassroomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(otherClassrooms);

            // Create and return combined DTO
            return new ClassUserModel
            {
                user = userdetailMapped,
                FriendRequests = friendRequestsMapped,
                UserRooms = userRoomsMapped,
                OtherClassrooms = otherClassroomsMapped
            };
        }

        public async Task<IEnumerable<TaskViewMeeting>> GetRoomByUserID(int userId)
        {
            var tasks = await _taskRepository.GetTaskByID(userId);
            var taskMap = _mapper.Map<IEnumerable<TaskViewMeeting>>(tasks);
            return taskMap;
        }
    }
}
