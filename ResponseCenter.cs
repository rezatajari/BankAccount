using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class ResponseCenter
    {
        public bool IsValid{ get;private set; }
        public string Message { get; private set; }

        private ResponseCenter(bool isValid,string message)
        {
            IsValid = isValid;
            Message = message;
        }


        public static ResponseCenter Success(string message = "Operation successful")
            => new ResponseCenter(isValid:true, message);

        public static ResponseCenter Fail(string message)
            => new ResponseCenter(isValid:false, message);
    }
}
