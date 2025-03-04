using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    /// <summary>
    /// Special class for validation of customer inputs and behaviour (SOC principle)
    /// </summary>
    public class AccountValidation
    {
        public enum TypeOfValidation
        {
            BankAccount,
            TransactionAccount
        }

        // name & amount represent of transaction or account context
        public static ResponseCenter ValidateInputs(string name, double amount,TypeOfValidation typeOfValidation)
        {
            // Amount check validation
            var isValidAmount = ValidationAmount(amount, typeOfValidation);
            if (!isValidAmount.IsValid)
            {
                return ResponseCenter.Fail(isValidAmount.Message);
            }

            // Name check validation
            var isValidName = ValidationName(name, typeOfValidation);
            if (!isValidName.IsValid)
            {
                return ResponseCenter.Fail(isValidName.Message);
            }
          
            // In the end response success if both of them is correct
            return ResponseCenter.Success();
        }

        // Validation for check doesn't negative input
        private static ResponseCenter ValidationAmount(double amount, TypeOfValidation typeOfValidation)
        {
            if (amount < 0)
            {
                var errorMessage = typeOfValidation switch
                {
                    TypeOfValidation.BankAccount => "Bank account amount must be positive",
                    TypeOfValidation.TransactionAccount => "Transaction amount must be positive",
                    _ => "Amount must be positive"
                };

                return ResponseCenter.Fail(errorMessage);
            }

            return ResponseCenter.Success();
        }


        // Validation for correct string input
        private static ResponseCenter ValidationName(string name, TypeOfValidation typeOfValidation)
        {
            if (!string.IsNullOrEmpty(name)) return ResponseCenter.Success();

            var errorMessage = typeOfValidation switch
            {
                TypeOfValidation.BankAccount => "Account name cannot empty.",
                TypeOfValidation.TransactionAccount => "Transaction name cannot empty",
                _ => "Name must be enter correct"
            };

            return ResponseCenter.Fail(errorMessage);

        }



    }
}
