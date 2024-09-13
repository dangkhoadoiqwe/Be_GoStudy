using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.ViewModel.ZaloPay
{
    public class CreateZalopayPayResponse
    {
        public int returnCode { get; set; }
        public string returnMessage { get; set; } = string.Empty;
        public string orderUrl { get; set; } = string.Empty;
    }
}
