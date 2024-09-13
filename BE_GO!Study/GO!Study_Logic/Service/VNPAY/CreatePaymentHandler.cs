using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GO_Study_Logic.Service.Interface;
using GO_Study_Logic.ViewModel.BaseErrors;
using GO_Study_Logic.ViewModel.Contants;
using GO_Study_Logic.ViewModel;
using DataAccess.Model;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using GO_Study_Logic.ViewModel.ZaloPay;
using GO_Study_Logic.Service.Momo;

namespace GO_Study_Logic.Service.VNPAY
{
    public class CreatePayment : IRequest<BaseResultWithDatas<PaymentLink>>
    {
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentCurrency { get; set; } = string.Empty;
        public string PaymentRefId { get; set; } = string.Empty;
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
        public string? PaymentLanguage { get; set; } = string.Empty;
        public string? MerchantId { get; set; } = string.Empty;
        public string? PaymentDestinationId { get; set; } = string.Empty;
        public string? Signature { get; set; } = string.Empty;

        // Thêm PackageId
        public string? PackageId { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; } = string.Empty;

        // Thêm UserId
        public int? UserId { get; set; }
    }

    public class CreatePaymentHandler : IRequestHandler<CreatePayment, BaseResultWithDatas<PaymentLink>>
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly VnpayConfig _vnpayConfig;
       
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public CreatePaymentHandler(
            IPaymentTransactionRepository paymentTransactionRepository,
            IOptions<VnpayConfig> vnpayConfigOptions,
           
            IUserRepository userRepository, IConfiguration configuration)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
            _vnpayConfig = vnpayConfigOptions.Value;
           
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<BaseResultWithDatas<PaymentLink>> Handle(CreatePayment request, CancellationToken cancellationToken)
        {
            var result = new BaseResultWithDatas<PaymentLink>();

            try
            {
                // Validate UserId
                if (request.UserId == null)
                {
                    result.Set(false, "UserId is required", null);
                    return result;
                }

                // Check if user exists in the system
                var user = await _userRepository.GetByIdAsync(request.UserId.Value);
                if (user == null)
                {
                    result.Set(false, "User not found", null);
                    return result;
                }

                // Validate PackageId
                if (string.IsNullOrEmpty(request.PackageId))
                {
                    result.Set(false, "PackageId is required", null);
                    return result;
                }

                // Tạo biến lưu URL thanh toán
                string paymentUrl = string.Empty;

                // Tạo transaction ID cho đơn hàng
                var transactionId = DateTime.Now.ToString("yyMMdd") + "_" + Guid.NewGuid().ToString();

                // Xử lý ZaloPay
                if (request.PaymentDestinationId?.ToUpper() == "ZALOPAY")
                {
                    var zalopayPayRequest = new CreateZalopayPayRequest(
                        2554,
                       "Demo",
                        DateTimeOffset.Now.ToUnixTimeMilliseconds(),
                        (long)request.RequiredAmount!,
                        transactionId,
                        "zalopayapp",
                        request.PaymentContent ?? string.Empty
                    );

                    zalopayPayRequest.MakeSignature("sdngKKJmqEMzvh5QQcdD2A9XBSKUNaYn");
                    (bool createZaloPayLinkResult, string? createZaloPayMessage) = zalopayPayRequest.GetLink("https://sandbox.zalopay.com.vn/v001/tpe/createorder");

                    if (createZaloPayLinkResult)
                    {
                        paymentUrl = createZaloPayMessage;
                    }
                    else
                    {
                        result.Set(false, createZaloPayMessage);
                        return result;
                    }
                }
                // Xử lý VNPay
                else if (request.PaymentDestinationId?.ToUpper() == "VNPAY")
                {
                    var vnpayPayRequest = new VnpayPayRequest(
                        _configuration["VNpay:Version"], _configuration["VNpay:tmnCode"],
                        DateTime.Now,
                        "127.0.0.1", // Client IP
                        request.RequiredAmount ?? 0,
                        request.PaymentCurrency ?? "VND",
                        "other",
                        request.PaymentContent ?? string.Empty,
                        _configuration["VNpay:ReturnUrl"],
                        transactionId,
                        request.ExpireDate
                    );

                    paymentUrl = vnpayPayRequest.GetLink(_configuration["VNpay:BaseUrl"],
                                                         _configuration["VNpay:HashSecret"]);
                }
                else if (request.PaymentDestinationId?.ToUpper() == "MOMO")
                {
                    var momoPayRequest = new MomoOneTimePaymentRequest(
                       "MOMOBKUN20180529",
                        transactionId, // Request ID
                        (long)request.RequiredAmount!, // Amount
                        transactionId, // Order ID (you can reuse the transactionId here)
                        request.PaymentContent ?? string.Empty, // Order Info
                        "https://localhost:7168/api/payment/momo-return", // Redirect URL
                        "https://localhost:7168/payment/api/momo-ipn", // Client IP
                        "captureWallet", // Request Type (use the appropriate type for your case)
                        string.Empty // Extra Data (you can replace this with actual data if required, otherwise an empty string)
                    );
                    momoPayRequest.MakeSignature("klm05TvNBzhg7h7j", "at67qH6mk8w5Y1nAyMoYKMWACiEi2bsa");
                    var (success, link) = momoPayRequest.GetLink("https://test-payment.momo.vn/v2/gateway/api/create");

                    if (success && link != null)
                    {
                        paymentUrl = link;
                    }
                    else
                    {
                        result.Set(false, "Invalid PaymentDestinationId", null);
                        return result;
                    }

                    // Lưu giao dịch vào database
                    // Tạo đối tượng PaymentTransaction để lưu thông tin giao dịch
                    var paymentTransaction = new PaymentTransaction
                    {
                        PaymentRefId = transactionId, // Mã giao dịch
                        Amount = request.RequiredAmount ?? 0, // Số tiền của giao dịch
                        PaymentMethod = request.PaymentDestinationId?.ToUpper(), // Phương thức thanh toán (VNPay, ZaloPay, ...)
                        Status = "Pending", // Trạng thái mặc định của giao dịch
                        PaymentLastMessage = "", // Tin nhắn cuối cùng từ hệ thống thanh toán (nếu có)
                        TransactionDate = DateTime.Now, // Ngày giờ tạo giao dịch
                        ExpireDate = request.ExpireDate, // Ngày hết hạn của giao dịch
                        PaymentContent = request.PaymentContent, // Nội dung thanh toán
                        PaymentCurrency = request.PaymentCurrency ?? "VND", // Tiền tệ của giao dịch (mặc định là VND)
                        PaymentLanguage = request.PaymentLanguage ?? "vn", // Ngôn ngữ của thanh toán
                        MerchantId = request.MerchantId ?? string.Empty, // ID của Merchant (Người bán)
                        PaymentDestinationId = request.PaymentDestinationId ?? string.Empty, // ID của điểm thanh toán (VNPay, ZaloPay, ...)
                        PackageId = request.PackageId != null ? int.Parse(request.PackageId) : 0, // Package ID (nếu có)
                        UserId = request.UserId ?? 0, // User ID (người tạo giao dịch)
                    };

                    await _paymentTransactionRepository.AddTransactionAsync(paymentTransaction);

                    // Trả về kết quả
                    result.Set(true, "Payment Link Generated Successfully", new PaymentLink()
                    {
                        PaymentId = transactionId,
                        PaymentUrl = paymentUrl
                    });
                }
            }
            catch (SqlException sqlEx)
            {
                result.Set(false, "Database Error", null);
                result.Errors.Add(new BaseError() { Code = "SqlException", Message = sqlEx.Message });
            }
            catch (Exception ex)
            {
                result.Set(false, "Exception Occurred", null);
                result.Errors.Add(new BaseError() { Code = "Exception", Message = ex.Message });
            }

            return result;
        }
    }
}
