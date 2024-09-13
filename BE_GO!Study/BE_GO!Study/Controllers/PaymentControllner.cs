using GO_Study_Logic.Service.VNPAY;
using GO_Study_Logic.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BE_GO_Study.Controllers
{
     
    [ApiController]
[Route("api/[controller]")]
public class PaymentControllner : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public PaymentControllner(IPaymentService paymentService, IMediator mediator, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _mediator = mediator;
            _configuration = configuration;
        }
        [HttpPost]
        [ProducesResponseType(typeof(BaseResultWithData<PaymentTransactionViewModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] GO_Study_Logic.Service.VNPAY.CreatePayment request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(request);

            if (!response.Success)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }
        [HttpGet("vnpay-return")]
        public IActionResult VnPayReturn([FromQuery] VnPayReturnRequest request)
        {
            var vnp_HashSecret = _configuration["Vnpay:HashSecret"];

            // 1. Kiểm tra secure hash để xác thực dữ liệu trả về
            var vnp_SecureHash = request.vnp_SecureHash;
            var paymentUrl = _paymentService.GenerateQueryString(request); // Tạo lại query string từ các tham số trả về để tạo chữ ký
            var validHash = GenerateSecureHash(paymentUrl, vnp_HashSecret); // Tạo lại secure hash từ các tham số trả về

            if (vnp_SecureHash != validHash)
            {
                return BadRequest("Invalid payment response");
            }

            // 2. Kiểm tra trạng thái giao dịch (vnp_TransactionStatus)
            if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
            {
                // Xử lý khi giao dịch thành công
                return Ok(new { message = "Payment successful", transactionId = request.vnp_TransactionNo });
            }
            else
            {
                // Xử lý khi giao dịch thất bại
                return BadRequest(new { message = "Payment failed", responseCode = request.vnp_ResponseCode });
            }
        }

       

        private string GenerateSecureHash(string queryString, string hashSecret)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(System.Text.Encoding.UTF8.GetBytes(hashSecret)))
            {
                var hashBytes = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(queryString));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertPaymentTransaction([FromBody] PaymentTransactionViewModel dto)
        {
            var transaction = await _paymentService.InsertPaymentTransactionAsync(dto);

            if (transaction == null)
            {
                return BadRequest("Failed to insert payment transaction");
            }

            return Ok(transaction);
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
    }

}
