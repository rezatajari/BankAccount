using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    /// <summary>
    /// Represents a bank account. Manages account details and transactions.
    /// </summary>
    public class BankAccount
    {
        // For unique account number generator
        private static int _accountNumberGenerator = 100;
        public BankAccount(string accountHolderName, double initBalance)
        {
            var result = BankAccountValidator.ValidateBalance(initBalance);
            if (!result.IsValid)
            {
                Console.WriteLine(result.Message);
                return; // Next the runtime app without throw exception 
            }

            AccountNumber = "Account Number Is: " + _accountNumberGenerator++;
            AccountHolderName = accountHolderName;
            Balance = initBalance;
        }

        // Properties 

        // It is readonly because we want to don't customer to set account number
        public string AccountNumber { get; }
        public string AccountHolderName { get; set; }
        // It is private because a customer can't set to balance with manually from out of class
        public double Balance { get; private set; }

        // Methods
        public void Deposit(double amount)
        {
        }

        public void Withdraw(double amount)
        {

        }

        public void CheckBalance() { }
    }
}
