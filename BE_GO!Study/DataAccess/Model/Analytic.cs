using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Model
{
    public class Analytic
    {
        [Key]
        public int AnalyticsId { get; set; }

        [Required]
        public string Metric { get; set; }

        [Required]
        public string Value { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public Tasks Task { get; set; }

        public int ClassroomId { get; set; }

        [ForeignKey("ClassroomId")]
        public Classroom Classroom { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
