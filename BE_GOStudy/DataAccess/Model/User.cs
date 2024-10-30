using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string ProfileImage { get; set; }

        public DateTime birthday { get; set; }

        public string sex { get; set; }

        public string phone { get; set; }
        public bool isonline { get; set; } = false;

        public int? PrivacySettingId { get; set; }
        [ForeignKey("PrivacySettingId")]
        public PrivacySetting PrivacySetting { get; set; }

        public ICollection<UserSpecialization> UserSpecializations { get; set; }

        public int? SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }

        public ICollection<FriendRequest> SentFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Data> Data { get; set; }
        public ICollection<Analytic> Analytics { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }

        public ICollection<UserLike> UserLikes { get; set; }
        // Removed direct relationship with
    }
}