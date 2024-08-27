using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IUserService
    {
        Task<User_View_Home_Model> GetHomeUserID(int userid);

        Task<UserProfileModel> GetUserProfile(int userid);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISemestersRepository _semestersRepository;
        private readonly ISpecializationRepository _specializationRepository;
        public UserService(IUserRepository userRepository, IMapper mapper, ISemestersRepository semestersRepository
            , ISpecializationRepository specializationRepository )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _semestersRepository = semestersRepository;
            _specializationRepository = specializationRepository;
        }

        public async Task<User_View_Home_Model> GetHomeUserID(int userid)
        {
            var user = await _userRepository.GetByIdAsync(userid);
            if (user == null)
            {
                return null; // Handle user not found scenario
            }
            int totalattendance = await _userRepository.TotalAttendance(userid);
            var attendances = await _userRepository.GetUserIdAtendanceAsync(userid);
            var blogPost = await _userRepository.Get1BlogPostAsync(userid);
            var friendRequests = await _userRepository.GetAllFriendRequestsAsync(userid);
            var rankings = await _userRepository.GetAllRankingAsync();
            var privacySetting = await _userRepository.GetPrivacySettingByuserIDAsync(userid);
            var anlyst = await _userRepository.GetUserIdAnalyticAsync(userid);


            var attendanceViewModels = _mapper.Map<List<Attendance_View_Model>>(attendances);
            var blogPostViewModel = _mapper.Map<BlogPost_View_Model>(blogPost);
            var friendRequestViewModels = _mapper.Map<List<FriendRequest_View_Model>>(friendRequests);
            var rankingViewModels = _mapper.Map<List<Ranking_View_Model>>(rankings);
            var privacySettingViewModel = _mapper.Map<PrivacySetting_View_Model>(privacySetting);
            var anaylystViewModel = _mapper.Map<List<Analytic_View_Model>>(anlyst);

            var userViewHomeModel = new User_View_Home_Model
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email, 
                ProfileImage = user.ProfileImage,
                BlogPost = blogPostViewModel,
                Rankings = rankingViewModels, 
                Attendances = attendanceViewModels,
                PrivacySetting = privacySettingViewModel,
                Analytics = anaylystViewModel,
                FriendRequests = friendRequestViewModels,
                totalAttendace = totalattendance
            };

            return userViewHomeModel;
        }

        public async Task<UserProfileModel> GetUserProfile(int userid)
        {
            var user = await _userRepository.GetByIdAsync(userid);
            if (user == null)
            {
                return null; // Handle user not found scenario
            }
            var semsterss = await _semestersRepository.GetByIdAsync(user.SemesterId);
            var Specialization = await _specializationRepository.GetByIdAsync(user.SpecializationId);
            var privacySetting = await _userRepository.GetPrivacySettingByuserIDAsync(userid);


            var SpecializationViewModel = _mapper.Map<Specialization_View_Model>(Specialization);
            var semsterViewModel = _mapper.Map<Semester_View_Model>(semsterss);
            var privacySettingViewModel = _mapper.Map<PrivacySetting_View_Model>(privacySetting);
            var userProfile = new UserProfileModel
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Semester  = semsterViewModel,
                Email = user.Email,
                Specialization = SpecializationViewModel,
                ProfileImage = user.ProfileImage,
                PrivacySetting = privacySettingViewModel,
                PasswordHash = user.PasswordHash 
            };
            return userProfile;
        }
    }
}
