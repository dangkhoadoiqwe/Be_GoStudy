using BE_GOStudy.AppStart;
using GO_Study_Logic.Service.Momo;
using GO_Study_Logic.Service.VNPAY;
using GO_Study_Logic.ViewModel;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Payment.Application.Features.Commands;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BE_GOStudy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public PaymentController(IPaymentService paymentService, IMediator mediator, IConfiguration configuration)
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

            // 1. Verify the secure hash to validate the payment response
            var vnp_SecureHash = request.vnp_SecureHash;
            var paymentUrl = _paymentService.GenerateQueryString(request); // Create query string from response parameters to generate the signature
            var validHash = GenerateSecureHash(paymentUrl, vnp_HashSecret); // Generate secure hash from response parameters

            if (vnp_SecureHash != validHash)
            {
                return BadRequest("Invalid payment response");
            }

            // 2. Check the transaction status (vnp_TransactionStatus)
            if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
            {
                // Handle successful transaction
                return Ok(new { message = "Payment successful", transactionId = request.vnp_TransactionNo });
            }
            else
            {
                // Handle failed transaction
                return BadRequest(new { message = "Payment failed", responseCode = request.vnp_ResponseCode });
            }
        }

  

        private string GenerateSecureHash(string queryString, string hashSecret)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(hashSecret)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryString));
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
        [HttpGet]
        [Route("momo-return")]
        public async Task<IActionResult> MomoReturn([FromQuery] MomoOneTimePaymentResultRequest response)
        {
            string returnUrl = string.Empty;
            var returnModel = new PaymentReturnDtos();
            var processResult = await _mediator.Send(response.Adapt<ProcessMomoPaymentReturn>());

            if (processResult.Success)
            {
                returnModel = processResult.Data.Item1 as PaymentReturnDtos;
                returnUrl = processResult.Data.Item2 as string;
            }

            if (returnUrl.EndsWith("/"))
                returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            return Redirect($"{returnUrl}?{returnModel.ToQueryString()}");
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
