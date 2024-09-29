using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel.User
{
    
    
    public class UserProfileModel
    {
        public int UserId { get; set; } 
        public string FullName { get; set; } 
        public Semester_View_Model Semester { get; set; }
        public List<Specialization_View_Model> Specialization { get; set; }

        public PrivacySetting_View_Model PrivacySetting { get; set; }
        public string Email { get; set; }  
        
        public string PasswordHash { get; set; }

        public int role { get; set; }
        public DateTime birthday { get; set; }

        public string sex { get; set; }

        public string phone { get; set; }

        public string ProfileImage { get; set; }

         
    }
    public class updateUserProfileModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
 
        public string PasswordHash { get; set; }


        public DateTime Birthday { get; set; }

        public string Sex { get; set; }

        public string Phone { get; set; }

        public string ProfileImage { get; set; }
    }
}
