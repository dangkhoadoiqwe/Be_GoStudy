using AutoMapper;
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

        Task UpdateClassroomLinkYtbUrlAsync(int classroomId, string linkUrl);
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
         

        public async Task UpdateClassroomLinkYtbUrlAsync(int classroomId, string linkUrl)
        {
            await _repository.UpdateClassroomLinkYtbUrlAsync(classroomId, linkUrl);
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
            var userdetail = await _userRepository.GetByIdAsync(userid);
            var friendRequests = await _userRepository.GetAllFriendRequestsAsync(userid);
            var classrooms = await _repository.GetOtherClassroomsAsync(userid);
            var userDashboard = await _repository.GetUserRoomAsync(userid);

            var reci = await _userRepository.GetAllFriendRecipientsAsync(userid);
            // Lấy danh sách bạn bè từ FriendRequest
            var friendRequestsList = await _userRepository.GetFriendsListAsync(userid);

            // Ánh xạ từ FriendRequest sang FriendViewModel
            var friendsModel = friendRequestsList.Select(fr =>
            {
                // Nếu userId là người gửi (Requester), hiển thị người nhận (Recipient)
                if (fr.Requester.UserId == userid)
                {
                    return new FriendViewModel
                    {
                        Requester = null, // Không cần hiển thị thông tin người gửi
                        Recipient = new UserViewModel
                        {
                            UserId = fr.Recipient.UserId,
                            FullName = fr.Recipient.FullName,
                            Email = fr.Recipient.Email,
                            ProfileImage = fr.Recipient.ProfileImage
                        }
                    };
                }
                // Nếu userId là người nhận (Recipient), hiển thị người gửi (Requester)
                else if (fr.Recipient.UserId == userid)
                {
                    return new FriendViewModel
                    {
                        Recipient = null, // Không cần hiển thị thông tin người nhận
                        Requester = new UserViewModel
                        {
                            UserId = fr.Requester.UserId,
                            FullName = fr.Requester.FullName,
                            Email = fr.Requester.Email,
                            ProfileImage = fr.Requester.ProfileImage
                        }
                    };
                }

                // Trường hợp không khớp (có thể không bao giờ xảy ra nếu chỉ lấy những request liên quan đến userid)
                return null;
            }).Where(fr => fr != null).ToList();



            var userRoomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(userDashboard);
            var userdetailMapped = _mapper.Map<UserViewModel>(userdetail);
            var otherClassroomsMapped = _mapper.Map<IEnumerable<ClassroomModel>>(classrooms);
            var friendRequestsMapped = _mapper.Map<IEnumerable<FriendRequest_View_Model>>(friendRequests);
            return new AllClassModel
            {
                user = userdetailMapped,
                FriendRequests = friendRequestsMapped,
                ClassUser = userRoomsMapped,
                Classroom = otherClassroomsMapped,
                ListFriend = friendsModel,
                FriendRecipient = _mapper.Map<List<FriendRequest_View_Model>>(reci)
            };
        }

        public async Task<ClassUserModel> GetUserDashboardAsync(int userId)
        {
            var friendRequests = await _userRepository.GetAllFriendRequestsAsync(userId);
            var userdetail = await _userRepository.GetByIdAsync(userId);
            var userRooms = await _repository.GetUserRoomAsync(userId);
            var otherClassrooms = await _repository.GetOtherClassroomsAsync(userId);

            var reci = await _userRepository.GetAllFriendRecipientsAsync(userId);
            // Lấy danh sách bạn bè từ FriendRequest
            var friendRequestsList = await _userRepository.GetFriendsListAsync(userId);

            // Ánh xạ từ FriendRequest sang FriendViewModel
            var friendsModel = friendRequestsList.Select(fr =>
            {
                // Nếu userId là người gửi (Requester), hiển thị người nhận (Recipient)
                if (fr.Requester.UserId == userId)
                {
                    return new FriendViewModel
                    {
                        Requester = null, // Không cần hiển thị thông tin người gửi
                        Recipient = new UserViewModel
                        {
                            UserId = fr.Recipient.UserId,
                            FullName = fr.Recipient.FullName,
                            Email = fr.Recipient.Email,
                            ProfileImage = fr.Recipient.ProfileImage
                        }
                    };
                }
                // Nếu userId là người nhận (Recipient), hiển thị người gửi (Requester)
                else if (fr.Recipient.UserId == userId)
                {
                    return new FriendViewModel
                    {
                        Recipient = null, // Không cần hiển thị thông tin người nhận
                        Requester = new UserViewModel
                        {
                            UserId = fr.Requester.UserId,
                            FullName = fr.Requester.FullName,
                            Email = fr.Requester.Email,
                            ProfileImage = fr.Requester.ProfileImage
                        }
                    };
                }

                // Trường hợp không khớp (có thể không bao giờ xảy ra nếu chỉ lấy những request liên quan đến userid)
                return null;
            }).Where(fr => fr != null).ToList();


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
                OtherClassrooms = otherClassroomsMapped,
                ListFriend = friendsModel,
                FriendRecipient = _mapper.Map<List<FriendRequest_View_Model>>(reci)
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
