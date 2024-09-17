using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.Contants;
using GO_Study_Logic.ViewModel.BaseErrors;
using DataAccess.Model;

namespace GO_Study_Logic.Service.VNPAY
{
    // Request để xử lý phản hồi từ VNPAY
    public class ProcessVnpayPaymentReturn : VnpayPayResponse, IRequest<BaseResultWithDatas<PaymentReturnDtos>>
    {
        // Không cần thêm gì ở đây vì các thuộc tính đã có trong VnpayPayResponse
    }

    // Handler để xử lý phản hồi VNPAY và cập nhật kết quả
    public class ProcessVnpayPaymentReturnHandler : IRequestHandler<ProcessVnpayPaymentReturn, BaseResultWithDatas<PaymentReturnDtos>>
    {
        private readonly GOStudyContext _context; // Database context
        private readonly VnpayConfig _vnpayConfig; // Cấu hình VNPAY

        public ProcessVnpayPaymentReturnHandler(GOStudyContext context, IOptions<VnpayConfig> vnpayConfigOptions)
        {
            _context = context;
            _vnpayConfig = vnpayConfigOptions.Value;
        }

        public async Task<BaseResultWithDatas<PaymentReturnDtos>> Handle(ProcessVnpayPaymentReturn request, CancellationToken cancellationToken)
        {
            var result = new BaseResultWithDatas<PaymentReturnDtos>();
            var resultData = new PaymentReturnDtos();

            try
            {
                // Kiểm tra chữ ký bảo mật từ phản hồi của VNPAY
                var isValidSignature = request.IsValidSignature(_vnpayConfig.HashSecret);

                if (isValidSignature)
                {
                    // Chuyển đổi vnp_TxnRef từ string sang int
                    if (int.TryParse(request.vnp_TxnRef, out int transactionId))
                    {
                        // Tìm giao dịch thanh toán bằng TransactionId
                        var payment = await _context.PaymentTransactions
                            .FirstOrDefaultAsync(p => p.TransactionId == transactionId);

                        if (payment != null)
                        {
                            if (request.vnp_ResponseCode == "00") // Thanh toán thành công
                            {
                                resultData.PaymentStatus = "00";
                                resultData.PaymentId = payment.TransactionId.ToString();

                                // Tạo chữ ký cho phản hồi, hiện tại dùng GUID tạm
                                resultData.Signature = Guid.NewGuid().ToString();

                                // Cập nhật trạng thái giao dịch
                                payment.Status = "Success";
                                payment.PaymentLastMessage = "Payment successful";
                                _context.PaymentTransactions.Update(payment);
                                await _context.SaveChangesAsync();
                            }
                            else // Thanh toán thất bại
                            {
                                resultData.PaymentStatus = "10";
                                resultData.PaymentMessage = "Payment process failed";
                            }
                        }
                        else
                        {
                            resultData.PaymentStatus = "11";
                            resultData.PaymentMessage = "Can't find payment at payment service";
                        }

                        result.Set(true, MessageContants.OK, resultData);
                    }
                    else
                    {
                        result.Set(false, "Invalid transaction ID format.");
                    }
                }
                else
                {
                    resultData.PaymentStatus = "99";
                    resultData.PaymentMessage = "Invalid signature in response";
                    result.Set(false, "Invalid signature.", resultData);
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
