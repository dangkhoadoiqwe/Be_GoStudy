using System;
using System.Collections.Generic;
using DataAccess.Model;

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

    public class BlogPost_View_Model
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class FriendRequest_View_Model
    {
        public int FriendRequestId { get; set; }
        public int RequesterId { get; set; }
        public int RecipientId { get; set; }
        public int Status { get; set; }

        public DateTime SentAt { get; set; }
    }
    public class Analytic_View_Model
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
    public class Semester_View_Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
    public class Specialization_View_Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class PrivacySetting_View_Model
    {
        public int PrivacySettingId { get; set; }
        public string Visibility { get; set; }

    }
    public class User_View_Home_Model
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
       
        public string ProfileImage { get; set; }

        
        public PrivacySetting_View_Model PrivacySetting { get; set; }  

        public List<Analytic_View_Model> Analytics { get; set; } = new List<Analytic_View_Model>(); 

        public List<Attendance_View_Model> Attendances { get; set; } = new List<Attendance_View_Model>(); 

        public List<Ranking_View_Model> Rankings { get; set; } = new List<Ranking_View_Model>();

        public List<FriendRequest_View_Model> FriendRequests { get; set; } = new List<FriendRequest_View_Model>();

        public BlogPost_View_Model BlogPost { get; set; } 
    }
}