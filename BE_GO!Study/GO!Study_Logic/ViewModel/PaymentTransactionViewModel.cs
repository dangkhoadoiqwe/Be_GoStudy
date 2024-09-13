using MediatR;
using System;
using System.Collections.Generic;
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
