using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service.VNPAY
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    
    using DataAccess.Model;
    using global::GO_Study_Logic.ViewModel;

    namespace GO_Study_Logic.Service.VNPAY
    {
        public class ProcessVnpayPaymentIpn : VnpayPayResponse, IRequest<BaseResultWithData<VnpayPayIpnResponse>>
        {
        }

        public class ProcessVnpayPaymentIpnHandler : IRequestHandler<ProcessVnpayPaymentIpn, BaseResultWithData<VnpayPayIpnResponse>>
        {
            private readonly GOStudyContext _context;
            private readonly ICurrentUserService _currentUserService;
            private readonly VnpayConfig _vnpayConfig;

            public ProcessVnpayPaymentIpnHandler(GOStudyContext context,
                                                 ICurrentUserService currentUserService,
                                                 IOptions<VnpayConfig> vnpayConfigOptions)
            {
                _context = context;
                _currentUserService = currentUserService;
                _vnpayConfig = vnpayConfigOptions.Value;
            }

            public async Task<BaseResultWithData<VnpayPayIpnResponse>> Handle(ProcessVnpayPaymentIpn request, CancellationToken cancellationToken)
            {
                var result = new BaseResultWithData<VnpayPayIpnResponse>();
                var resultData = new VnpayPayIpnResponse();

                try
                {
                    var isValidSignature = request.IsValidSignature(_vnpayConfig.HashSecret);

                    if (isValidSignature)
                    {
                        // Lấy thông tin thanh toán bằng TransactionId (vnp_TxnRef)
                        if (int.TryParse(request.vnp_TxnRef, out int transactionId))
                        {
                            var payment = await _context.PaymentTransactions
                                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);

                            if (payment != null)
                            {
                                // Kiểm tra số tiền có khớp không
                                if (payment.Amount == (request.vnp_Amount / 100))
                                {
                                    // Kiểm tra trạng thái giao dịch
                                    if (payment.Status != "0") // Trạng thái chưa xử lý
                                    {
                                        string message;
                                        string status;

                                        if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
                                        {
                                            status = "0"; // Thành công
                                            message = "Transaction success";
                                        }
                                        else
                                        {
                                            status = "-1"; // Lỗi
                                            message = "Transaction error";
                                        }

                                        // Cập nhật thông tin giao dịch
                                        payment.Status = status;
                                        payment.PaymentLastMessage = message;
                                        payment.TransactionDate = DateTime.Now;

                                        _context.PaymentTransactions.Update(payment);
                                        await _context.SaveChangesAsync();

                                        // Xác nhận thành công
                                        resultData.Set("00", "Confirm success");
                                    }
                                    else
                                    {
                                        resultData.Set("02", "Order already confirmed");
                                    }
                                }
                                else
                                {
                                    resultData.Set("04", "Invalid amount");
                                }
                            }
                            else
                            {
                                resultData.Set("01", "Order not found");
                            }
                        }
                        else
                        {
                            resultData.Set("99", "Invalid TransactionId format");
                        }
                    }
                    else
                    {
                        resultData.Set("97", "Invalid signature");
                    }
                }
                catch (Exception ex)
                {
                    resultData.Set("99", "Exception occurred: " + ex.Message);
                }

                result.Data = resultData;
                return result;
            }
        }
    }

}
