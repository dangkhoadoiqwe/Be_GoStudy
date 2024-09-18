using MediatR;
using Microsoft.Extensions.Configuration;
 
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Model;
using GO_Study_Logic.Service.Momo;
using GO_Study_Logic.Service.VNPAY;
using GO_Study_Logic.ViewModel.BaseErrors;
using GO_Study_Logic.ViewModel.Contants;
using GO_Study_Logic.ViewModel;

namespace Payment.Application.Features.Commands
{
    public class ProcessMomoPaymentReturn : MomoOneTimePaymentResultRequest,
        IRequest<BaseResultWithData<(PaymentReturnDtos, string)>>
    {
    }

    public class ProcessMomoPaymentReturnHandler
        : IRequestHandler<ProcessMomoPaymentReturn, BaseResultWithData<(PaymentReturnDtos, string)>>
    {
        private readonly GOStudyContext _context;
        private readonly IConfiguration _configuration;

        public ProcessMomoPaymentReturnHandler(GOStudyContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<BaseResultWithData<(PaymentReturnDtos, string)>> Handle(ProcessMomoPaymentReturn request, CancellationToken cancellationToken)
        {
            var result = new BaseResultWithData<(PaymentReturnDtos, string)>();
            var resultData = new PaymentReturnDtos();

            try
            {
                var accessKey = _configuration["Momo:AccessKey"];
                var secretKey = _configuration["Momo:SecretKey"];
                var returnUrlBase = _configuration["Momo:ReturnUrl"];

                var isValidSignature = request.IsValidSignature(accessKey, secretKey);

                if (isValidSignature)
                {
                    var payment = await _context.PaymentTransactions
                        .Where(p => p.TransactionId.ToString() == request.orderId)
                        .Select(p => new
                        {
                            p.TransactionId,
                            p.Amount
                        })
                        .SingleOrDefaultAsync();

                    if (payment != null)
                    {
                        var returnUrl = returnUrlBase;

                        if (request.resultCode == 0)
                        {
                            resultData.PaymentStatus = "00";
                            resultData.PaymentId = payment.TransactionId.ToString();
                            resultData.Signature = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            resultData.PaymentStatus = "10";
                            resultData.PaymentMessage = "Payment process failed";
                        }

                        result.Success = true;
                        result.Message = MessageContants.OK;
                        result.Data = (resultData, returnUrl);
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Can't find payment at payment service";
                       
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "Invalid signature in response";
                   
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = MessageContants.Error;
               
            }

            return result;
        }
    }
}