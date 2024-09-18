using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Reaction
    {
        [Key]
        public int ReactionId { get; set; }

        public int MessageId { get; set; }
        [ForeignKey("MessageId")]
        public Message Message { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string Emoji { get; set; }
    }
}
