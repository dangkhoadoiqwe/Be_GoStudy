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

        public string Status { get; set; }

        public string PaymentLastMessage { get; set; } = string.Empty;

        // Thêm các trường bổ sung theo yêu cầu
        public string PaymentContent { get; set; } = string.Empty; // Nội dung thanh toán
        public string PaymentCurrency { get; set; } = "VND"; // Tiền tệ thanh toán
        public string PaymentRefId { get; set; } = string.Empty; // Mã tham chiếu thanh toán
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddMinutes(15); // Ngày hết hạn
        public string PaymentLanguage { get; set; } = "vn"; // Ngôn ngữ thanh toán
        public string MerchantId { get; set; } = string.Empty; // ID của người bán
        public string PaymentDestinationId { get; set; } = string.Empty; // ID của điểm đến thanh toán

        
    }

}
