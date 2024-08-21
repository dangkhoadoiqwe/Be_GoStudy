using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public string PasswordHash { get; set; }

        public string ProfileImage { get; set; }

        public int? PrivacySettingId { get; set; }
        [ForeignKey("PrivacySettingId")]
        public PrivacySetting PrivacySetting { get; set; }

        public int? SpecializationId { get; set; }
        [ForeignKey("SpecializationId")]
        public Specialization Specialization { get; set; }

        public int? SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public Semester Semester { get; set; }
        public ICollection<FriendRequest> SentFriendRequests { get; set; }
        public ICollection<FriendRequest> ReceivedFriendRequests { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
         public ICollection<Comment> Comments { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public ICollection<Data> Data { get; set; }

        public ICollection<Analytic> Analytics { get; set; }

        public ICollection<Attendance> Attendances { get; set; }

    }
}
