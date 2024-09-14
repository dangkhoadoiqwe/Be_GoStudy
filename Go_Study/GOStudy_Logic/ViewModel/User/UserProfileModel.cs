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
        public Specialization_View_Model Specialization { get; set; }

        public PrivacySetting_View_Model PrivacySetting { get; set; }
        public string Email { get; set; }  
        
        public string PasswordHash { get; set; }

        public string ProfileImage { get; set; }
    }
}
