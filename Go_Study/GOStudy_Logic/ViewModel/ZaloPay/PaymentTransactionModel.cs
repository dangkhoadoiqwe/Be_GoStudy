using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel.ZaloPay
{
    public class PaymentTransactionModel
    {
        public int Id { get; set; } // Unique identifier for the transaction (Primary Key)

        public string OrderId { get; set; } = string.Empty; // Transaction Order ID

        public decimal Amount { get; set; } // The amount of the transaction

        public string PaymentMethod { get; set; } = string.Empty; // Payment method (VNPay, ZaloPay, etc.)

        public string PaymentUrl { get; set; } = string.Empty; // URL for payment or QR code

        public DateTime CreatedDate { get; set; } // The date and time when the transaction was created

        public string Status { get; set; } = "Pending"; // The status of the transaction (Pending, Success, Failed)
    }
}
