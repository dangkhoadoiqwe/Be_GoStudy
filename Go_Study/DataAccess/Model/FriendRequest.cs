using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class FriendRequest
    {
        [Key]
        public int FriendRequestId { get; set; }

        public int RequesterId { get; set; }
        [ForeignKey("RequesterId")]
        public User Requester { get; set; }

        public int RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime SentAt { get; set; }
    }
}
