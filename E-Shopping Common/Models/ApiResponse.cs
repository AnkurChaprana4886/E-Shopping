using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_Common.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; }
        public string Message { get; }
        public T Data { get; }
        public ApiResponse(int statusCode, string message, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
