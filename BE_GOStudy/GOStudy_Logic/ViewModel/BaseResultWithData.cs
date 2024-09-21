using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GO_Study_Logic.ViewModel.BaseErrors;
namespace GO_Study_Logic.ViewModel
{
    public class BaseResultWithDatas<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public List<BaseError> Errors { get; private set; } = new List<BaseError>();

        public void Set(bool success, string message, T data = default(T))
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}