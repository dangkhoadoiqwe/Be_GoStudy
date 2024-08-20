using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class Classroom
    {
        [Key]
        public int ClassroomId { get; set; }

        [Required]
        public string Name { get; set; }

        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User User { get; set; }

        public string Nickname { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Automatically set to current time

        public ICollection<Message> Messages { get; set; }

        // Optional: Linking to analytics
        public ICollection<Analytic> Analytics { get; set; }
    }
}
