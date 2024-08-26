using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel
{
    public class ClassroomModel
    {
        public int ClassroomId { get; set; } 
      
        public string Name { get; set; }

        public int CreatedBy { get; set; } 

        public string Nickname { get; set; }

        public DateTime CreatedAt { get; set; } 
    }

    public class ClassUserModel
    {
        public IEnumerable<FriendRequest> FriendRequests { get; set; }
        public IEnumerable<ClassroomModel> UserRooms { get; set; }
        public IEnumerable<ClassroomModel> OtherClassrooms { get; set; }
    }
}
