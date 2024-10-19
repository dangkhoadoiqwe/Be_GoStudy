using System;
using System.Collections.Generic;
using DataAccess.Model;
using static DataAccess.Repositories.UserRepository;

namespace GO_Study_Logic.ViewModel.User
{
    public class Ranking_View_Model
    {
        public int RankId { get; set; }
        public string UserName { get; set; }
        public decimal PerformanceScore { get; set; }
        public int RankPosition { get; set; }
        public string Period { get; set; }
    }

    public class Attendance_View_Model
    {
        public int AttendanceId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
        public string Notes { get; set; }

        public int UserId { get; set; }
    }

  

    public class FriendRequest_View_Model
    {
        public int FriendRequestId { get; set; }
        public UserViewModel Requester { get; set; }
      //  public int RequesterId { get; set; }
        public UserViewModel Recipient { get; set; }
 //       public int RecipientId { get; set; }
        public string Status { get; set; }

        public DateTime SentAt { get; set; }
    }
    public class Analytic_View_Model
    {
        public int AnalyticsId { get; set; }
        public string Description { get; set; }

        public int Metric { get; set;  }
        public decimal Value { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int ClassroomId { get; set; }
        public DateTime Timestamp { get; set; }
    }
    
  
    public class PrivacySetting_View_Model
    {
        public int PrivacySettingId { get; set; }
        public string Visibility { get; set; }

    }

    public class FriendViewModel
    {
        public UserViewModel? Requester { get; set; } // Thông tin người gửi
        public UserViewModel? Recipient { get; set; } // Thông tin người nhận
    }

    public class User_View_Home_Model
    {
        public int UserId { get; set; }

     //   public int role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
       
        public string ProfileImage { get; set; }

        public string PakageUser { get; set; }

        public int totalAttendace { get; set; }
        public BlogPost_View_Model BlogPost { get; set; }
        public PrivacySetting_View_Model PrivacySetting { get; set; }

        public List<UserViewModel> AllFriends { get; set; } = new List<UserViewModel>();
        public List<Analytic_View_Model> Analytics { get; set; } = new List<Analytic_View_Model>(); 

        public List<Attendance_View_Model> Attendances { get; set; } = new List<Attendance_View_Model>(); 

        public List<Ranking_View_Model> Rankings { get; set; } = new List<Ranking_View_Model>();
        public List<FriendViewModel> ListFriend { get; set; } = new List<FriendViewModel>();
        public List<FriendRequest_View_Model> FriendRequests { get; set; } = new List<FriendRequest_View_Model>();
        public List<FriendRequest_View_Model> FriendRecipient { get; set; } = new List<FriendRequest_View_Model>();

        public List<SpecializationUserDetailViewModel> SpecializationUserDetails { get; set; } = new List<SpecializationUserDetailViewModel>();


        public List<TaskViewModel> taskViewModels { get; set; } = new List<TaskViewModel>();
    }
}