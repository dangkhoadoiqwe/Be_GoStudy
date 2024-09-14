using DataAccess.Model;
using GO_Study_Logic.ViewModel.BaseErrors;
using GO_Study_Logic.ViewModel.Contants;
using GO_Study_Logic.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GO_Study_Logic.Service.VNPAY
{
    public class GetPayment : IRequest<BaseResultWithDatas<PaymentDtos>>
    {
        public string Id { get; set; } = string.Empty;
    }

    // Handler để xử lý truy vấn lấy thông tin thanh toán
    public class GetPaymentHandler : IRequestHandler<GetPayment, BaseResultWithDatas<PaymentDtos>>
    {
        private readonly GOStudyContext _context; // Sử dụng DbContext cho cơ sở dữ liệu

        public GetPaymentHandler(GOStudyContext context)
        {
            _context = context;
        }

        public async Task<BaseResultWithDatas<PaymentDtos>> Handle(GetPayment request, CancellationToken cancellationToken)
        {
            var result = new BaseResultWithDatas<PaymentDtos>();

            try
            {
                // Tìm giao dịch thanh toán dựa trên PaymentId
                var payment = await _context.PaymentTransactions
       .Where(p => p.TransactionId.ToString() == request.Id)
       .Select(p => new PaymentDtos
       {
           PaymentId = p.TransactionId.ToString(),
           RequiredAmount = p.Amount,
           PaymentDate = p.TransactionDate,
           PaymentMethod = p.PaymentMethod,
           Status = p.Status,
           PaymentLastMessage = p.PaymentLastMessage
       })
       .FirstOrDefaultAsync();


                if (payment != null)
                {
                    result.Set(true, MessageContants.OK, payment);
                }
                else
                {
                    result.Set(false, MessageContants.NotFound);
                }
            }
            catch (Exception ex)
            {
                result.Set(false, MessageContants.Error);
                result.Errors.Add(new BaseError
                {
                    Code = MessageContants.Exception,
                    Message = ex.Message,
                });
            }

            return result;
        }
    }
}
