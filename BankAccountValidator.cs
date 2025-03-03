using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    /// <summary>
    /// Special class for validation of customer inputs and behaviour (SOC principle)
    /// </summary>
    public class BankAccountValidator
    {
        public static ResponseCenter ValidateBalance(double balance)
        {
            if (balance < 0)
              return  ResponseCenter.Fail("Balance can not negative!");

            return ResponseCenter.Success();
        }
    }
}
