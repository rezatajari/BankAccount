using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Model
{
    public class Account
    {
        // For unique account number generator
        private static int _accountNumberGenerator = 100;
        private static int _idGen = 100;
        private const string BankBrandName = "AccountBankOfAmerican";

        public Account(int customerId, string customerName, double initBalance)
        {
            var result = AmountValidation.IsValidAmount(initBalance);
            if (!result.IsValid)
            {
                Console.WriteLine(result.Message);
                return; // Next the runtime app without throw exception 
            }

            AccountHolderName = $"{customerName}+ {BankBrandName}";
            AccountNumber = "BankCenter: " + _accountNumberGenerator++;
            Balance = initBalance;
            CustomerId = customerId;
            Id = _idGen++;

        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string AccountNumber { get; private set; }
        public string AccountHolderName { get; private set; }
        // It is private because a customer can't set to balance with manually from out of class
        public double Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
