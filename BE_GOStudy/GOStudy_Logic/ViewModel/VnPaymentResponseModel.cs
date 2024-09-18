using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel
{
    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderDescription { get; set; }
        public string PackageId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
    public class VnpayConfig
    {
        public static string ConfigName => "Vnpay";  // Tên section trong file cấu hình (appsettings.json)

        // Phiên bản API của VNPAY (ví dụ: "2.1.0")
        public string Version { get; set; } = string.Empty;

        // Mã TmnCode của Merchant trên VNPAY
        public string TmnCode { get; set; } = string.Empty;

        // Chuỗi bảo mật HashSecret dùng để tạo chữ ký
        public string HashSecret { get; set; } = string.Empty;

        // URL trả về sau khi thanh toán hoàn tất
        public string ReturnUrl { get; set; } = string.Empty;

        // URL base của hệ thống thanh toán VNPAY (ví dụ: sandbox hoặc production)
        public string BaseUrl { get; set; } = string.Empty;

        // Mã tiền tệ sử dụng trong giao dịch (ví dụ: "VND")
        public string CurrCode { get; set; } = "VND";

        // Ngôn ngữ mặc định (ví dụ: "vn")
        public string Locale { get; set; } = "vn";

        // URL thanh toán (có thể khác nhau giữa sandbox và production)
        public string PaymentUrl { get; set; } = string.Empty;

        // URL được VNPAY gọi lại sau khi thanh toán hoàn tất
        public string PaymentBackReturnUrl { get; set; } = string.Empty;
    }
    public class PaymentDtos
    {
        public string PaymentId { get; set; } // ID giao dịch thanh toán
        public decimal RequiredAmount { get; set; } // Số tiền cần thiết
        public DateTime PaymentDate { get; set; } // Ngày giao dịch
        public string PaymentMethod { get; set; } // Phương thức thanh toán
        public string Status { get; set; } // Trạng thái thanh toán
        public string PaymentLastMessage { get; set; } // Tin nhắn cuối cùng về thanh toán
    }

    public class VnPayReturnRequest
    {
        public string vnp_Amount { get; set; }
        public string vnp_BankCode { get; set; }
        public string vnp_BankTranNo { get; set; }
        public string vnp_CardType { get; set; }
        public string vnp_OrderInfo { get; set; }
        public string vnp_PayDate { get; set; }
        public string vnp_ResponseCode { get; set; }
        public string vnp_TmnCode { get; set; }
        public string vnp_TransactionNo { get; set; }
        public string vnp_TransactionStatus { get; set; }
        public string vnp_TxnRef { get; set; }
        public string vnp_SecureHash { get; set; }
    }
    public class VnpayPayIpnResponse
    {

        public VnpayPayIpnResponse()
        {

        }
        public VnpayPayIpnResponse(string rspCode, string message)
        {
            RspCode = rspCode;
            Message = message;
        }
        public void Set(string rspCode, string message)
        {
            RspCode = rspCode;
            Message = message;
        }
        public string RspCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }   
    public class PaymentConstants
    {
        public static string InsertSprocName => "sproc_PaymentInsert";
        public static string SelectByIdSprocName => "sproc_PaymentSelectById";
    }

    public class VnPaymentRequestModel
    {
        public int PackageId { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
