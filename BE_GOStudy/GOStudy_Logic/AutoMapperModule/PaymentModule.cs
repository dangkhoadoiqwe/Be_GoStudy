using AutoMapper;
using DataAccess.Model;
using GO_Study_Logic.ViewModel;
using GO_Study_Logic.ViewModel.ZaloPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.AutoMapperModule
{
    public static class PaymentModule
    {
        public static void ConfigPaymentModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<PaymentTransaction, PaymentTransactionModel>().ReverseMap();
             

                            mc.CreateMap<PaymentTransaction, PaymentStatusViewModel>().ReverseMap();

        }
    }
}
