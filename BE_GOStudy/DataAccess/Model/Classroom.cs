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

        public string Name { get; set; }

        public int SpecializationId { get; set; }
        [ForeignKey("SpecializationId")]
        public Specialization Specialization { get; set; }

        public string Nickname { get; set; }
        public string LinkUrl { get; set; }

        public string? YoutubeUrl { get; set; }

        public int status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Message> Messages { get; set; }
        public ICollection<Analytic> Analytics { get; set; }

        // Removed navigation to Users
    }
}
