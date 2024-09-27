using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public  class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
         
        public string Token { get; set; } = null!;
        public DateTime IssuedAt { get; set; }
        public DateTime ExpriedAt { get; set; }
        public string JwtId { get; set; } = null!;
        public bool IsUsed { get; set; }
    //    public string AccessTokenGoogle { get; set; } = null!;
        public bool IsRevoked { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")] 
        public User User { get; set; } = null!;
    }
}
