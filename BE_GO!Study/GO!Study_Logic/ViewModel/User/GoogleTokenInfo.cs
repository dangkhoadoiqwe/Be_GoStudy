using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel.User
{
    // Model for the original Google Token Info
    public class GoogleTokenInfo
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Sub { get; set; }  // User ID
        public string Aud { get; set; }  // Audience
        public string Iss { get; set; }  // Issuer
        public long Exp { get; set; }    // Expiration time
    }

    public class CustomTokenInfo
    {
        public Context Context { get; set; }
        public string Aud { get; set; }  // Audience
        public string Iss { get; set; }  // Issuer
        public string Sub { get; set; }  // Subject
        public string Room { get; set; } // Room
        public long Exp { get; set; }    // Expiration time
    }

    public class Context
    {
        public UserInfo User { get; set; }
        public string Group { get; set; }
    }

    public class UserInfo
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }

    public class GoogleLoginRequest
    {
        public string IdToken { get; set; }
    }

    // Đổi tên class từ 'User' thành 'AppUser'
    public class AppUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }

      
    }




}
