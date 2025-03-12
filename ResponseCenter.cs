using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankAccount
{
    /// <summary>
    /// A general response entire all of an application for better to friendly response 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseCenter<T>
    {
        public bool IsValid { get; private set; }
        public string Message { get; private set; }
        public T Data { get; set; }
        private ResponseCenter(bool isValid, string message,T data=default)
        {
            IsValid = isValid;
            Message = message;
            Data = data;
        }


        public static ResponseCenter<T> Success(string message = "Operation successful", T data = default)
            => new ResponseCenter<T>(isValid: true, message,data);

        public static ResponseCenter<T> Fail(string message)
            => new ResponseCenter<T>(isValid: false, message);
    }
}
