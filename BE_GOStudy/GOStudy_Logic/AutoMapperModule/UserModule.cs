using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.User;
using GOStudy_Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.AutoMapperModule
{
    public static class UserModule
    {
        public static void ConfigUserModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<User, UserProfileModel>().ReverseMap();
            mc.CreateMap<User, UserViewModel>().ReverseMap();
            mc.CreateMap<User, UserViewModel1>().ReverseMap();
            mc.CreateMap<User, User_View_Home_Model>().ReverseMap();
            mc.CreateMap<User, Ranking_View_Model>().ReverseMap();
            mc.CreateMap<User, GoogleTokenInfo>().ReverseMap();
            mc.CreateMap<User, AppUser>().ReverseMap();
            mc.CreateMap<User, TokenModel>().ReverseMap();

            mc.CreateMap<User, updateUserProfileModel>().ReverseMap();
            mc.CreateMap<Analytic, Analytic_View_Model>().ReverseMap();

            mc.CreateMap<Attendance, Attendance_View_Model>().ReverseMap(); 

            mc.CreateMap<BlogPost, BlogPost_View_Model>().ReverseMap(); 

            mc.CreateMap<FriendRequest, FriendRequest_View_Model>().ReverseMap(); 

            mc.CreateMap<Ranking, Ranking_View_Model>().ReverseMap();

            mc.CreateMap<PrivacySetting, PrivacySetting_View_Model>().ReverseMap();

            mc.CreateMap<Attendance, AttendanceRequestModel>().ReverseMap();

            mc.CreateMap<Specialization, SpecializationViewModel>().ReverseMap();
        }
    }
}
