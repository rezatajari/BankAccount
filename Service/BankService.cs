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

        /// <summary>
        /// List of customers in bank
        /// </summary>
        public List<Customer> Customers { get; private set; } = [];
        /// <summary>
        /// List of account in bank
        /// </summary>
        public List<Account> Accounts { get; private set; } = [];
        /// <summary>
        /// List of transaction in bank
        /// </summary>
        public List<Transaction> Transactions { get; private set; } = [];

        /// <summary>
        /// Initial to create new account
        /// </summary>
        /// <returns></returns>
        public ResponseCenter<int> CreateNewAccount()
        {
            // Create new customer
            var newCustomer = CreateNewCustomer();

            // Create new account
            var accountId = CreateAccount(newCustomer);
            return ResponseCenter<int>.Success(message: "Account created", data: accountId);
        }
        /// <summary>
        /// Get account from bank
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private ResponseCenter<Account> GetAccount(int accountId)
        {
            var account = Accounts.FirstOrDefault(a => a.Id == accountId);
            return account == null ? ResponseCenter<Account>.Fail("Your account is not exist") : ResponseCenter<Account>.Success(data: account);
        }
        /// <summary>
        /// Create new account for a customer
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns></returns>
        private int CreateAccount(Customer newCustomer)
        {
            var newAccount = new Account(newCustomer.Id, newCustomer.Name, newCustomer.InitialBalance);
            Accounts.Add(newAccount);
            return newAccount.Id;
        }
        /// <summary>
        /// Create new customer information for bank
        /// </summary>
        /// <returns></returns>
        private Customer CreateNewCustomer()
        {
            var customer = _customerService.CreateCustomerInformation();
            Customers.Add(customer);
            return customer;
        }
        /// <summary>
        /// Deposit of bank account of a customer 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amountDeposit"></param>
        /// <returns></returns>
        public ResponseCenter<bool> Deposit(int accountId, double amountDeposit)
        {
            var account = GetAccount(accountId).Data;

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
        /// <summary>
        /// Withdraw of bank account of a customer
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="amountWithdraw"></param>
        /// <returns></returns>
        public ResponseCenter<bool> Withdraw(int accountId, double amountWithdraw)
        {
            var account = GetAccount(accountId).Data;

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
        /// <summary>
        /// Check balance of customer in own account
        /// </summary>
        /// <param name="accountId"></param>
        public void CheckBalance(int accountId)
        {
            var balance = Accounts.Where(a => a.Id == accountId)
                .Select(b => b.Balance)
                .FirstOrDefault();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Your balance is: {balance}\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Add transaction to list of transaction of a customer in bank
        /// </summary>
        /// <param name="tx"></param>
        /// <returns></returns>
        public ResponseCenter<bool> AddTransaction(Transaction tx)
        {
            var result = tx.TypeTx switch
            {
                TypeOfTx.Deposit => Deposit(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.InputTransferFromOthers => Deposit(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.BuySomething => Withdraw(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.Withdraw => Withdraw(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.OutputTransferToOthers => Withdraw(tx.AccountId, tx.TxPriceAmount),
                _ => ResponseCenter<bool>.Fail("We should be regular transaction")
            };

            // Doesn't successful transaction
            if (!result.IsValid)
                return result;

            // Successful transaction
            Transactions.Add(tx);
            return result;
        }
        /// <summary>
        /// Get list of transaction a customer from in own account
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<Transaction> GetTransactions(int accountId)
        {
            return Transactions.Where(i => i.AccountId == accountId).ToList();
        }
    }
}
