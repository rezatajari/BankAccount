using BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Service
{
    /// <summary>
    /// Represents a bank account. Manages account details and transactions.
    /// </summary>
    public class BankService
    {
        private readonly CustomerService _customerService = new();

        public List<Customer> Customers { get; private set; } = [];
        public List<Account> Accounts { get; private set; } = [];

        public ResponseCenter<int> CreateNewAccount()
        {
            // Create new customer
            var newCustomer = CreateNewCustomer();

            // Create new account
            var accountId = CreateAccount(newCustomer);
            return ResponseCenter<int>.Success(message: "Account created", data: accountId);
        }


        public ResponseCenter<Account> GetAccount(int accountId)
        {
            var account = Accounts.FirstOrDefault(a => a.Id == accountId);
            return account == null ? ResponseCenter<Account>.Fail("Your account is not exist") : ResponseCenter<Account>.Success(data: account);
        }


        private int CreateAccount(Customer newCustomer)
        {
            var newAccount = new Account(newCustomer.Id, newCustomer.Name, newCustomer.InitialBalance);
            Accounts.Add(newAccount);
            return newAccount.Id;
        }

        private Customer CreateNewCustomer()
        {
            var customer = _customerService.CreateCustomerInformation();
            Customers.Add(customer);
            return customer;
        }

        public ResponseCenter<bool> Deposit(int accountId, double amountDeposit)
        {
            var account = Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                return ResponseCenter<bool>.Fail("Account not found");

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
            return ResponseCenter<bool>.Success($"Your deposit is successfully and you balance account now is: {account.Balance}");
        }

        public ResponseCenter<bool> Withdraw(int accountId, double amountWithdraw)
        {
            var account = Accounts.FirstOrDefault(a => a.Id == accountId);
            if (account == null)
                return ResponseCenter<bool>.Fail("Account not found");

            var amountValidation = AmountValidation.IsValidAmount(amountWithdraw);
            if (!amountValidation.IsValid)
                Console.WriteLine(amountValidation.Message);

            if (account.Balance > amountWithdraw)
            {
                account.Balance -= amountWithdraw;
                return ResponseCenter<bool>.Success($"Your balance is now: {account.Balance}");
            }
            else
            {
                return ResponseCenter<bool>.Fail("Account balance is not enough.");
            }
        }
        public void CheckBalance(int accountId)
        {
            var balance = Accounts.Where(a => a.Id == accountId)
                .Select(b => b.Balance)
                .FirstOrDefault();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Your balance is: {balance}\n");
            Console.ResetColor();
        }
    }
}
