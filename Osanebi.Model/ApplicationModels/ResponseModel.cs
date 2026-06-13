using System;
using System.Collections.Generic;
using System.Text;

namespace Osanebi.Model.ApplicationModels
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ResponseModel()
        {
            IsSuccess = true;
            Message = string.Empty;
            Data = default(T);
        }
        public ResponseModel(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
    }
}
