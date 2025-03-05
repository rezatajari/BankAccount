using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    /// <summary>
    /// Special class for validation of customer balance (SOC principle)
    /// </summary>
    public class AmountValidation
    {
        // Validation for check doesn't negative input
        public static ResponseCenter IsValidAmount(double amount)
        {
            return amount < 0 ? ResponseCenter.Fail("Bank account amount must be positive") : ResponseCenter.Success();
        }

    }
}
