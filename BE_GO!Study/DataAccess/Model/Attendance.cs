using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime Date { get; set; } // Ngày điểm danh

        public bool IsPresent { get; set; } // Trạng thái điểm danh (Có mặt hoặc vắng mặt)

        public string Notes { get; set; } // Ghi chú (nếu có)
    }

}
