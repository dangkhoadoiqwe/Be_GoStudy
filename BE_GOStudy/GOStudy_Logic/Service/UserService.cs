using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service
{
    public interface IUserService

    {
        Task<User_View_Home_Model> GetHomeUserID(int userid);
        AppUser ConvertToAppUser(User user);
        Task<UserProfileModel> GetUserProfile(int userid);

        Task<User> FindOrCreateUser(GoogleTokenInfo googleTokenInfo);

        Task<UserViewModel> GetById(int userId);
        string GenerateJwtToken(CustomTokenInfo customTokenInfo, string secretKey);

        Task<bool> checktoken(int userid);

        Task<bool> UpdateUserProfileAsync(int userId, updateUserProfileModel userProfileModel);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ISemestersRepository _semestersRepository;
        private readonly ISpecializationRepository _specializationRepository;
        public readonly ITaskRepository _taskRepository;
        public readonly IPackageRepository _packageRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, ISemestersRepository semestersRepository
            , ISpecializationRepository specializationRepository ,ITaskRepository taskRepository, IPackageRepository packageRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _semestersRepository = semestersRepository;
            _specializationRepository = specializationRepository;
            _taskRepository = taskRepository;
            _packageRepository = packageRepository;
        }
        public async Task<UserViewModel> GetById(int userId)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.UserId == userId);
            var userModel = _mapper.Map<UserViewModel>(user);
            return userModel;
        }
        public AppUser ConvertToAppUser(User user)
        {
            return new AppUser
            {
                FullName = user.FullName,
                Email = user.Email,
                ProfileImage = user.ProfileImage,
               UserId = user.UserId,  
                                      
            };
        }
        public async Task<bool> UpdateUserProfileAsync(int userId, updateUserProfileModel userProfileModel)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new Exception("User not found.");
                }

                // Only update fields that are not default or placeholder values
                user.FullName = !string.IsNullOrEmpty(userProfileModel.FullName) && userProfileModel.FullName != "string"
                                ? userProfileModel.FullName
                                : user.FullName;

                user.PasswordHash = !string.IsNullOrEmpty(userProfileModel.PasswordHash) && userProfileModel.PasswordHash != "string"
                                    ? userProfileModel.PasswordHash
                                    : user.PasswordHash;

                user.ProfileImage = !string.IsNullOrEmpty(userProfileModel.ProfileImage) && userProfileModel.ProfileImage != "string"
                                    ? userProfileModel.ProfileImage
                                    : user.ProfileImage;

                user.phone = !string.IsNullOrEmpty(userProfileModel.Phone) && userProfileModel.Phone != "string"
                             ? userProfileModel.Phone
                             : user.phone;

                user.birthday = userProfileModel.Birthday != DateTime.MinValue
                                ? userProfileModel.Birthday
                                : user.birthday;

                user.sex = !string.IsNullOrEmpty(userProfileModel.Sex) && userProfileModel.Sex != "string"
                           ? userProfileModel.Sex
                           : user.sex;

                // Update the user in the repository
                await _userRepository.UpdateUserAsync(user);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user profile: {ex.Message}");
                throw;  // Re-throw the exception after logging it
            }
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
            var tasks = await _taskRepository.GetTaskByUserIdForToday(userid);
            var packageUser = await _packageRepository.GetPackageNamesByUserIdAsync(userid);
            var GetSpecializationDetails = await _userRepository.GetSpecializationDetailsByUserIdAsync(userid);
            var specializationUserDetails = GetSpecializationDetails.ToList();
            var taskviewmodel = _mapper.Map<List<TaskViewModel>>(tasks);
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
                PakageUser = packageUser != null && packageUser.Any() ? string.Join(", ", packageUser) : "NO package",
                SpecializationUserDetails = specializationUserDetails,
                BlogPost = blogPostViewModel,
                Rankings = rankingViewModels, 
                Attendances = attendanceViewModels,
                PrivacySetting = privacySettingViewModel,
                Analytics = anaylystViewModel,
                FriendRequests = friendRequestViewModels,
                taskViewModels = taskviewmodel,
                totalAttendace = totalattendance
            };

            return userViewHomeModel;
        }
        public async Task<User> FindOrCreateUser(GoogleTokenInfo googleTokenInfo)  // Thay đổi từ User thành AppUser
        {
            if (googleTokenInfo == null)
            {
                throw new ArgumentNullException(nameof(googleTokenInfo), "Google token info cannot be null");
            }

            // Check if the user already exists based on the email from Google
            var user = await _userRepository.GetUserByEmailAsync(googleTokenInfo.Email);
            if (user == null)
            {
                // If user does not exist, create a new AppUser
                user = new User
                {
                    FullName = googleTokenInfo.Name ?? "Default Name",
                    Email = googleTokenInfo.Email,
                    ProfileImage = googleTokenInfo.Picture,
                    PasswordHash = "123", // Consider using a secure password hash
                    PrivacySettingId = 1,
                    Role = 1,
                    SemesterId = 1,
                    phone ="NoPhone",
                    sex ="Nosex",
                    birthday =  DateTime.Today,
                };

                // Save the new AppUser to the database
                await _userRepository.CreateUserAsync(user);
            }

            return user;
        }
        public string GenerateJwtToken(CustomTokenInfo customTokenInfo, string secretKey)
        {
            // Ensure the secret key is at least 256 bits (32 characters)
            if (string.IsNullOrEmpty(secretKey) || Encoding.UTF8.GetBytes(secretKey).Length < 32)
            {
                throw new ArgumentOutOfRangeException(nameof(secretKey), "The secret key must be at least 256 bits (32 characters).");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTimeOffset.FromUnixTimeSeconds(customTokenInfo.Exp).UtcDateTime,
                SigningCredentials = creds,
                AdditionalHeaderClaims = new Dictionary<string, object>
        {
            { "context", new Dictionary<string, object>
                {
                    { "user", new Dictionary<string, object>
                        {
                            { "avatar", customTokenInfo.Context.User.Avatar },
                            { "name", customTokenInfo.Context.User.Name },
                            { "email", customTokenInfo.Context.User.Email },
                            { "id", customTokenInfo.Context.User.Id }
                        }
                    },
                    { "group", customTokenInfo.Context.Group }
                }
            },
            { "aud", customTokenInfo.Aud },
            { "iss", customTokenInfo.Iss },
            { "sub", customTokenInfo.Sub },
            { "room", customTokenInfo.Room },
            { "exp", customTokenInfo.Exp }
        }
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<UserProfileModel> GetUserProfile(int userid)
        {
            var user = await _userRepository.GetByIdAsync(userid);
            if (user == null)
            {
                return null; // Handle user not found scenario
            }
            var semsterss = await _semestersRepository.GetByIdAsync(user.SemesterId);
         var Specialization = await _specializationRepository.GetAllSpecializationsByUserIDAsync(userid);
            var privacySetting = await _userRepository.GetPrivacySettingByuserIDAsync(userid);
           
            var SpecializationViewModel = _mapper.Map<List<Specialization_View_Model>>(Specialization);
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
                PasswordHash = user.PasswordHash ,
                role = user.Role,
                birthday = user.birthday,
                sex = user.sex,
                phone = user.phone,
            };
            return userProfile;
        }

        public async Task<bool> checktoken(int userid)
        {
            var usercheck = await _userRepository.CheckToken(userid);
            return usercheck;
        }
    }
}
