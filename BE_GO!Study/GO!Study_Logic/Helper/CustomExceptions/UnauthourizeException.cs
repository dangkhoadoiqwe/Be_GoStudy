using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Helper.CustomExceptions
{
    public class UnauthourizeException : Exception
    {
        public UnauthourizeException()
        {
        }
        public UnauthourizeException(string? message) : base(message)
        {
        }
        public UnauthourizeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
