using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public int SenderId { get; set; }   // Sender
        [ForeignKey("SenderId")]
        public User Sender { get; set; }

        public int RecipientId { get; set; }  // Recipient
        [ForeignKey("RecipientId")]
        public User Recipient { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }

}
