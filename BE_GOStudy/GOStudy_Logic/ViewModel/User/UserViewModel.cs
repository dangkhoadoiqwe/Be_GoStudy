using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel.User
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public string PasswordHash { get; set; }
        public string ProfileImage { get; set; }

    }
}
