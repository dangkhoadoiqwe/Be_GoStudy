using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        public int TimeComplete {  get; set; }

        public bool IsDeleted { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime ScheduledTime { get; set; }

        public DateTime ScheduledEndTime { get; set; }

        [Required]
        public bool Status { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public ICollection<Analytic> Analytics { get; set; }
    }
}
