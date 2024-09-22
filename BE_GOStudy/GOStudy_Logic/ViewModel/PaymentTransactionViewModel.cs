using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel
{
    public class BaseResultWithData<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
 

public class PaymentTransactionDto
    {
        public int TransactionId { get; set; } // ID của giao dịch
        public decimal Amount { get; set; } // Số tiền thanh toán
        public string PaymentCurrency { get; set; } // Loại tiền tệ
        public string PaymentContent { get; set; } // Mô tả giao dịch
        public string PaymentRefId { get; set; } = string.Empty; // Mã tham chiếu thanh toán
        public int PackageID { get; set; } // ID của gói dịch vụ
        public int UserId { get; set; } // ID của người dùng
        public string PaymentMethod { get; set; } // Phương thức thanh toán 
        [Required]
        public string BuyerName { get; set; } // Tên người mua
        [Required]
        public string BuyerEmail { get; set; } // Email người mua
        [Required]
        public string BuyerPhone { get; set; } // Số điện thoại người mua

        [Required]
        public string Title { get; set; } // Tiêu đề giao dịch
        [Required]
        public string Description { get; set; } // Mô tả chi tiết về giao dịch
    }

    public class PaymentLink
    {
        public string PaymentId { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
    }
    public class PaymentTransactionViewModel
    {
        public string PaymentId { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int PackageId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
    }
    public class UpdateStatusDto
    {
        public int TransactionId { get; set; }
        public string Status { get; set; }
    }
}
