using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service.VNPAY
{
    public interface IPaymentService
    {
        Task<PaymentTransaction> InsertPaymentTransactionAsync(PaymentTransactionViewModel dto);
        Task<bool> UpdatePaymentTransactionStatusAsync(int transactionId, string status);
        string GenerateQueryString(VnPayReturnRequest request);
    }

    public class PaymentService : IPaymentService
    {
        private readonly GOStudyContext _context;

        public PaymentService(GOStudyContext context)
        {
            _context = context;
        }
        public  string GenerateQueryString(VnPayReturnRequest request)
        {
            var queryParams = new Dictionary<string, string>
        {
            { "vnp_Amount", request.vnp_Amount },
            { "vnp_BankCode", request.vnp_BankCode },
            { "vnp_BankTranNo", request.vnp_BankTranNo },
            { "vnp_CardType", request.vnp_CardType },
            { "vnp_OrderInfo", request.vnp_OrderInfo },
            { "vnp_PayDate", request.vnp_PayDate },
            { "vnp_ResponseCode", request.vnp_ResponseCode },
            { "vnp_TmnCode", request.vnp_TmnCode },
            { "vnp_TransactionNo", request.vnp_TransactionNo },
            { "vnp_TransactionStatus", request.vnp_TransactionStatus },
            { "vnp_TxnRef", request.vnp_TxnRef }
        
        };

            var queryString = string.Join("&", queryParams.OrderBy(q => q.Key).Select(q => $"{q.Key}={q.Value}"));
            return queryString;
        }
        public async Task<PaymentTransaction> InsertPaymentTransactionAsync(PaymentTransactionViewModel dto)
        {
            var paymentTransaction = new PaymentTransaction
            {
                UserId = dto.UserId,
                PackageId = dto.PackageId,
                TransactionDate = DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                Status = "Pending"
            };

            _context.PaymentTransactions.Add(paymentTransaction);
            await _context.SaveChangesAsync();

            return paymentTransaction;
        }

        public async Task<bool> UpdatePaymentTransactionStatusAsync(int transactionId, string status)
        {
            var paymentTransaction = await _context.PaymentTransactions.FindAsync(transactionId);

            if (paymentTransaction == null)
            {
                return false;
            }

            paymentTransaction.Status = status;
            _context.PaymentTransactions.Update(paymentTransaction);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
