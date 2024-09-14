//using MediatR;
//using Microsoft.Extensions.Configuration;
//using Payment.Application.Base.Models;
//using Payment.Application.Constants;
//using Payment.Application.Features.Dtos;
//using Payment.Application.Interface;
//using Payment.Service.Momo.Request;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using DataAccess.Model;
//using GO_Study_Logic.Service.Momo;
//using GO_Study_Logic.Service.VNPAY;
//using GO_Study_Logic.ViewModel.BaseErrors;
//using GO_Study_Logic.ViewModel.Contants;
//using GO_Study_Logic.ViewModel;

//namespace Payment.Application.Features.Commands
//{
//    public class ProcessMomoPaymentReturn : MomoOneTimePaymentResultRequest,
//        IRequest<BaseResultWithData<(PaymentReturnDtos, string)>>
//    {
//    }

//    public class ProcessMomoPaymentReturnHandler
//        : IRequestHandler<ProcessMomoPaymentReturn, BaseResultWithData<(PaymentReturnDtos, string)>>
//    {
//        private readonly GOStudyContext _context;
//        private readonly IConfiguration _configuration;

//        public ProcessMomoPaymentReturnHandler(GOStudyContext context,
//            IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        public async Task<BaseResultWithData<(PaymentReturnDtos, string)>> Handle(ProcessMomoPaymentReturn request, CancellationToken cancellationToken)
//        {
//            var result = new BaseResultWithData<(PaymentReturnDtos, string)>();
//            var resultData = new PaymentReturnDtos();

//            try
//            {
//                // Retrieve Momo configuration from IConfiguration
//                var accessKey = _configuration["Momo:AccessKey"];
//                var secretKey = _configuration["Momo:SecretKey"];
//                var returnUrlBase = _configuration["Momo:ReturnUrl"]; // If needed

//                // Validate the signature
//                var isValidSignature = request.IsValidSignature(accessKey, secretKey);

//                if (isValidSignature)
//                {
//                    // Retrieve the payment transaction
//                    var payment = await _context.PaymentTransactions
//                        .Where(p => p.Id.ToString() == request.orderId)
//                        .Select(p => new
//                        {
//                            p.Id,
//                            p.Amount
//                            // Include other relevant properties if needed
//                        })
//                        .SingleOrDefaultAsync();

//                    if (payment != null)
//                    {
//                        // Set the return URL based on your logic
//                        var returnUrl = returnUrlBase; // or derive it from some logic

//                        if (request.resultCode == 0)
//                        {
//                            resultData.PaymentStatus = "00";
//                            resultData.PaymentId = payment.Id;
//                            // TODO: Generate the actual signature
//                            resultData.Signature = Guid.NewGuid().ToString();
//                        }
//                        else
//                        {
//                            resultData.PaymentStatus = "10";
//                            resultData.PaymentMessage = "Payment process failed";
//                        }

//                        result.Success = true;
//                        result.Message = MessageContants.OK;
//                        result.Data = (resultData, returnUrl);
//                    }
//                    else
//                    {
//                        result.Success = false;
//                        result.Message = "Can't find payment at payment service";
//                        result.Errors.Add(new BaseError
//                        {
//                            Code = "PaymentNotFound",
//                            Message = "Payment transaction not found in the database."
//                        });
//                    }
//                }
//                else
//                {
//                    result.Success = false;
//                    result.Message = "Invalid signature in response";
//                    result.Errors.Add(new BaseError
//                    {
//                        Code = "InvalidSignature",
//                        Message = "Signature validation failed."
//                    });
//                }
//            }
//            catch (Exception ex)
//            {
//                result.Success = false;
//                result.Message = MessageContants.Error;
//                result.Errors.Add(new BaseError
//                {
//                    Code = MessageContants.Exception,
//                    Message = ex.Message,
//                });
//            }

//            return result;
//        }
//    }
//}
