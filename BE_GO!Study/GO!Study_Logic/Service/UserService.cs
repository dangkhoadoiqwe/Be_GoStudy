using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
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
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User_View_Home_Model> GetHomeUserID(int userid)
        {
            var user = await _userRepository.GetByIdAsync(userid);
            if (user == null)
            {
                return null; // Handle user not found scenario
            }

            var attendances = await _userRepository.GetUserIdAtendanceAsync(userid);
            var blogPost = await _userRepository.Get1BlogPostAsync(userid);
            var friendRequests = await _userRepository.GetAllFriendRequestsAsync(userid);
            var rankings = await _userRepository.GetAllRankingAsync();
            var privacySetting = await _userRepository.GetPrivacySettingByuserIDAsync(userid);



            var attendanceViewModels = _mapper.Map<List<Attendance_View_Model>>(attendances);
            var blogPostViewModel = _mapper.Map<BlogPost_View_Model>(blogPost);
            var friendRequestViewModels = _mapper.Map<List<FriendRequest_View_Model>>(friendRequests);
            var rankingViewModels = _mapper.Map<List<Ranking_View_Model>>(rankings);
            var privacySettingViewModel = _mapper.Map<PrivacySetting_View_Model>(privacySetting);

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

                FriendRequests = friendRequestViewModels
            };

            return userViewHomeModel;
        }

    }
}
