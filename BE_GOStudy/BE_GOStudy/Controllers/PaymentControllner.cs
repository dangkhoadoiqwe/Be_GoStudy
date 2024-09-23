using DataAccess.Model;
using GO_Study_Logic.Service.VNPAY;
using GO_Study_Logic.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GO_Study_Logic.ViewModel.User;
using GO_Study_Logic.ViewModel.ZaloPay;
using DataAccess.Repositories;

namespace BE_GOStudy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly PayOS _payOS;
        private readonly ILogger<PaymentController> _logger;
        private readonly IUserRepository _userRepository;
public PaymentController(IPaymentService paymentService, IConfiguration configuration, PayOS payOS
    , ILogger<PaymentController> logger, IUserRepository userRepository)
{
       _paymentService = paymentService;
       _configuration = configuration;
       _payOS = payOS;
       _logger = logger;
       _userRepository = userRepository;
}

        // Define the PaymentData class

        [HttpPost("Payos")]
        public async Task<IActionResult> CreateTransaction([FromBody] PaymentTransactionDto transaction)
        {
            if (transaction == null)
                return BadRequest("Transaction cannot be null.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Create a new PaymentTransaction object to store in the database
            var paymentTransaction = new PaymentTransaction
            {
                Amount = transaction.Amount,
                PaymentCurrency = transaction.PaymentCurrency,
                PaymentContent = transaction.PaymentContent,
                PaymentRefId = transaction.PaymentRefId,
                PaymentMethod = transaction.PaymentMethod,
                Status = "Pending", // Default status
                TransactionDate = DateTime.UtcNow, // Current date and time
                PackageId = transaction.PackageID, // PackageId
                UserId = transaction.UserId
            };

            // Ensure that PaymentRefId is convertible to a smaller long value
            if (!long.TryParse(transaction.PaymentRefId, out long orderCode) || orderCode > 999999999999999)
            {
                return BadRequest("PaymentRefId is not in the correct format or is out of range.");
            }

            // Save the transaction to the database
            await _paymentService.AddTransactionAsync(paymentTransaction);

            // Create ItemData for PayOS
            var item = new ItemData(paymentTransaction.PaymentContent, 1, (int)paymentTransaction.Amount);
            List<ItemData> items = new List<ItemData> { item };

            // Log the order code for debugging
            _logger.LogInformation($"Order Code: {orderCode}");

            // Create PaymentData object for PayOS
            PaymentData paymentData = new PaymentData(
                orderCode: orderCode,
                amount: (int)paymentTransaction.Amount,
                description: paymentTransaction.PaymentContent,
                items: items,
                cancelUrl: "https://localhost:7173/api/Payment/payos-cancel",
                returnUrl: "https://localhost:7173/api/Payment/payos-return", // Ensure this is the correct endpoint
                buyerName: transaction.BuyerName, // Replace with actual user data from FE
                buyerEmail: transaction.BuyerEmail, // Replace with actual user data from FE
                buyerPhone: transaction.BuyerPhone, // Replace with actual user data from FE
                signature: "se161076" // You might want to calculate this dynamically based on the transaction
            );

            // Call PayOS to create the payment link
            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

            if (createPayment == null || string.IsNullOrEmpty(createPayment.checkoutUrl))
            {
                return StatusCode(500, "Failed to create payment link.");
            }

            return Ok(createPayment); // Return the payment link for the frontend to redirect to
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetPaymentByPaymentRefId(string code)
        {
            var paymentTransaction = await _paymentService.GetPaymentbyPaymentRefefid(code);
            if (paymentTransaction == null)
            {
                return NotFound("Payment transaction not found.");
            }
            return Ok(paymentTransaction);
        }


        [HttpGet("payos-cancel")]
        public async Task<IActionResult> CancelTransaction([FromQuery] string orderCode)
        {
            // Lấy thông tin giao dịch từ cơ sở dữ liệu bằng OrderCode
            var transaction = await _paymentService.GetTransactionByOrderCodeAsync(orderCode);
            if (transaction == null)
                return NotFound("Transaction not found.");

            // Kiểm tra trạng thái giao dịch để quyết định xem có thể hủy hay không
            if (transaction.Status != "Pending")
            {
                return BadRequest("Transaction cannot be canceled.");
            }

            // Cập nhật trạng thái giao dịch thành "Canceled"
            var success = await _paymentService.UpdatePaymentTransactionStatusAsync(transaction.TransactionId, "Canceled");

            if (!success)
            {
                return StatusCode(500, "Failed to update transaction status.");
            }

            return Ok("Transaction has been canceled.");
        }


        [HttpGet("payos-return")]
        public async Task<IActionResult> PayosReturn([FromQuery] PayosReturnRequest request)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(request.Code) || string.IsNullOrEmpty(request.Status) || string.IsNullOrEmpty(request.OrderCode))
            {
                return BadRequest("Code, Status, and OrderCode are required.");
            }

            var responseCode = request.Code; // Mã phản hồi từ PayOS
            var transactionStatus = request.Status; // Trạng thái giao dịch
            var orderCode = request.OrderCode; // Mã đơn hàng

            // Cập nhật trạng thái giao dịch trong cơ sở dữ liệu dựa trên OrderCode
            var success = await _paymentService.UpdatePaymentTransactionStatusByOrderCodeAsync(orderCode, transactionStatus);

            if (!success)
            {
                return NotFound("Transaction not found");
            }

            // JavaScript để gửi thông tin thanh toán về frontend
            var paymentResult = (responseCode == "00" && transactionStatus == "PAID") ? "true" : "false";

            string html = $@"
        <html>
        <body>
            <script>
                // Gửi kết quả thanh toán về frontend qua postMessage
                window.opener.postMessage({{ success: {paymentResult} }}, '*');
                window.close(); // Tự động đóng tab thanh toán
            </script>
        </body>
        </html>";

            return Content(html, "text/html");
        }




        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetTransactionStatus(int id)
        {
            var transaction = await _paymentService.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();

            var status = await _paymentService.GetPaymentStatus(transaction.TransactionId.ToString());
            return Ok(new { Status = status });
        }

   
        [HttpGet("vnpay-return")]
        public IActionResult VnPayReturn([FromQuery] VnPayReturnRequest request)
        {
            var vnp_HashSecret = _configuration["Vnpay:HashSecret"];
            var vnp_SecureHash = request.vnp_SecureHash;

            var paymentUrl = _paymentService.GenerateQueryString(request);
            var validHash = GenerateSecureHash(paymentUrl, vnp_HashSecret);

            if (vnp_SecureHash != validHash)
            {
                return BadRequest("Invalid payment response");
            }

            if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
            {
                return Ok(new { message = "Payment successful", transactionId = request.vnp_TransactionNo });
            }
            else
            {
                return BadRequest(new { message = "Payment failed", responseCode = request.vnp_ResponseCode });
            }
        }
        [HttpGet("checkout")]
        public async Task<IActionResult> Checkout(int userId, int packageId)
        {
            // Lấy thông tin người dùng
            var user = await _userRepository.GetByIdAsync(userId); // Phương thức này cần có trong dịch vụ
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Gọi phương thức thanh toán
            var package = await _paymentService.Checkout(userId, packageId); // Phương thức này cũng cần có
            if (package == null)
            {
                return NotFound("Package not found.");
            }

            return Ok(package);
        }


        [HttpPost]
        [Route("update-status")]
        public async Task<IActionResult> UpdatePaymentTransactionStatus([FromBody] UpdateStatusDto dto)
        {
            var success = await _paymentService.UpdatePaymentTransactionStatusAsync(dto.TransactionId, dto.Status);

            if (!success)
            {
                return NotFound("Transaction not found");
            }

            return Ok("Transaction status updated");
        }

        private string GenerateSecureHash(string queryString, string hashSecret)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hashSecret)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryString));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
