using GO_Study_Logic.Service.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service.VNPAY
{
    public class ConnectionService : IConnectionService
    {
        private readonly IConfiguration configuration;

        public ConnectionService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string? Datebase => configuration.GetConnectionString("DefaultConnection");
    }
}
