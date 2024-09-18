using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Ranking
    {
        [Key]
        public int RankId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public decimal PerformanceScore { get; set; }

        [Required]
        public int RankPosition { get; set; }

        [Required]
        public string Period { get; set; }

       
    }
}
