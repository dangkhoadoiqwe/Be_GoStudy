using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel
{
    public class TaskViewModel
    {
        public int TaskId {  get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public int TimeComplete { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string Status { get; set; } 

        public bool IsDeleted { get; set; }
    }

}
