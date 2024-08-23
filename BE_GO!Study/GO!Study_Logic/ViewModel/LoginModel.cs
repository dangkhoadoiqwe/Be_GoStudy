using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel
{
    public class LoginModel
    {
        public bool Success { get; set; } = false;
        public string UserName { get; set; }
        public string Role { get; set; }
        public object Data { get; set; } 
    }

    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class UserModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

    }
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class UserViewModel
    {
        public int UserId { get; set; } 
        public string FullName { get; set; } 
        public string Email { get; set; } 
        public int Role { get; set; }  
        public string PasswordHash { get; set; }

    }

    public enum RoleEnum
    {
        Manager = 1,
        Staff = 2,
        Admin = 3,
 
    }
}
