using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class PaymentTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int PackageId { get; set; }
        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
