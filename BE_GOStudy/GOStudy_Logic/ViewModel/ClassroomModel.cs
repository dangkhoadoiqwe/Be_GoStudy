using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GO_Study_Logic.ViewModel.User;

namespace GO_Study_Logic.ViewModel
{
    public class ClassroomModel
    {
        public int ClassroomId { get; set; } 
      
        public string Name { get; set; }
        public string Nickname { get; set; }

        public string LinkUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    public class ClassroomNameModel
    {
        public int ClassroomId { get; set; }

        public string Name { get; set; }
        
    }
    public class AllClassModel
    {
        public List<FriendRequest_View_Model> FriendRecipient { get; set; } = new List<FriendRequest_View_Model>();
        public List<FriendViewModel> ListFriend { get; set; } = new List<FriendViewModel>();
        public UserViewModel user { get; set; }
        public IEnumerable<FriendRequest_View_Model> FriendRequests { get; set; }
        public IEnumerable<ClassroomModel> ClassUser { get; set; }

        public IEnumerable<ClassroomModel> Classroom { get; set; }
    }
    public class NotificationViewRoom {
        public string Message { get; set; }

      
        public DateTime SentAt { get; set; }
    }

    public class TaskViewMeeting
    {
        public int TaskId { get; set; }

        public int UserId { get; set; }
    
        public string Title { get; set; }

        public int TimeComplete { get; set; }

        public string Description { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string Status { get; set; }

        //public IEnumerable<NotificationViewRoom> Notifications { get; set; }

   
}
 
    public class ClassUserModel
    {
        public UserViewModel user { get; set; }
        public IEnumerable<FriendRequest_View_Model> FriendRequests { get; set; }
        
        public IEnumerable<ClassroomModel> UserRooms { get; set; }
        public IEnumerable<ClassroomModel> OtherClassrooms { get; set; }

        public List<FriendRequest_View_Model> FriendRecipient { get; set; } = new List<FriendRequest_View_Model>();
        public List<FriendViewModel> ListFriend { get; set; } = new List<FriendViewModel>();
    }
}
