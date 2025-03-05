using BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Logic
{
    /// <summary>
    /// Represents a bank account. Manages account details and transactions.
    /// </summary>
    public class BankService
    {
        private readonly CustomerService _customerService;
        public BankService()
        {
            _customerService = new CustomerService();
        }

        public List<Customer> Customers { get; private set; } = [];
        public List<Account> Accounts { get; private set; } = [];

        public ResponseCenter CreateNewAccount(Customer newCustomer)
        {
            Customers.Add(newCustomer);
            var newAccount = new Account(newCustomer.Id, newCustomer.Name, newCustomer.InitialBalance);
            Accounts.Add(newAccount);
            return ResponseCenter.Success("Account created");
        }
        public ResponseCenter Deposit(int customerId, double amountDeposit)
        {
            var account = Accounts.FirstOrDefault(c => c.CustomerId == customerId);
            if (account==null)
                return ResponseCenter.Fail("Account not found");

            var amountValidation = AmountValidation.IsValidAmount(amountDeposit);
            if (!amountValidation.IsValid)
                Console.WriteLine(amountValidation.Message);

            /*
             How It Works
               Reference Types:
               
               In C#, objects (like Account) are reference types. This means that when you retrieve an object from a list (e.g., Accounts),
               you’re working with a reference to that object, not a copy.
               
               Any changes you make to the object’s properties (e.g., Balance) will affect the original object in the list.
               
               Updating the Balance:
               
               When you call account.Balance += amountDeposit, you’re modifying the Balance property of the specific Account object in the Accounts list.
               
               The Accounts list itself is not modified, but the object it contains is updated.
               */
            account.Balance += amountDeposit;
            return ResponseCenter.Success($"Your deposit is successfully and you balance account now is: {account.Balance}");
        }

        public ResponseCenter Withdraw(int customerId,double amountWithdraw)
        {
            var account = Accounts.FirstOrDefault(c => c.CustomerId == customerId);
            if (account == null)
                return ResponseCenter.Fail("Account not found");

            var amountValidation = AmountValidation.IsValidAmount(amountWithdraw);
            if (!amountValidation.IsValid)
                Console.WriteLine(amountValidation.Message);

            if (account.Balance > amountWithdraw)
            {
                account.Balance -= amountWithdraw;
                return ResponseCenter.Success($"Your balance is now: {account.Balance}");
            }
            else
            {
                return ResponseCenter.Fail("Account balance is not enough.");
            }
        }
        public ResponseCenter CheckBalance(int customerId)
        {
            var balance = Accounts.Where(c => c.CustomerId == customerId)
                .Select(b => b.Balance)
                .FirstOrDefault();

            return ResponseCenter.Success($"Your balance is: {balance}");
        }
    }
}
