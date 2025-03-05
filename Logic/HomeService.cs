using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccount.Model;

namespace BankAccount.Logic
{
    public class HomeService
    {
        public static void ApplicationRun()
        {
            // Welcome 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to bank center of american!:)\n");
            Console.ForegroundColor = ConsoleColor.White;

            // --------------------- Creation account process --------------------- //
            var isCreateAccount = AccountCreate();

            if (!isCreateAccount) Exit();

            // Get customer information
            var customer = CustomerService.GetCustomerInformation();
            // Run bank service
            var bankService = new BankService();
            // Create bank account
            var result=bankService.CreateNewAccount(customer);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result.Message+ "\n");
            Console.ForegroundColor = ConsoleColor.White;

            // --------------------- Create transaction process --------------------- //
            var isCreateTx = TransactionService();

            if(!isCreateTx) Exit();

            // Create new transaction
            var newTx = new Transaction(customer.Id, transactionName: "Apple", transactionPriceAmount: 100, TypeOfTx.BuySomething);

            // Run transaction service
            var txService = new TransactionService(bankService);
            txService.AddTransaction(newTx);

            // list of tx
            var transactions = txService.GetTransactions();
            foreach (var tx in transactions)
            {
                Console.WriteLine(tx.TxName);
                Console.WriteLine(tx.TxPriceAmount);
                Console.WriteLine(tx.DateOfTx);
                Console.WriteLine(tx.TypeTx);
            }

            var checkBalance= CheckBalance();
            if (!checkBalance) Exit();

            // Check balance
            var accountBalance=    bankService.CheckBalance(customer.Id);
            Console.WriteLine(accountBalance);



        }
        private static bool CheckBalance()
        {
            Console.WriteLine("Do you want check your balance? 1)Yes 2)No (Exit)\n");
            var result = Console.ReadLine();
            return result != null && result.Contains('1');
        }
        private static bool AccountCreate()
        {
            Console.WriteLine("Do you want to create new account!? 1)Yes 2)No (Exit!)\n");
            var result = Console.ReadLine();
            return result != null && result.Contains('1');
        }
        private static bool TransactionService()
        {
            Console.WriteLine("Do you have a new transaction? 1)Yes 2)No (Exit!)");
            var result = Console.ReadLine();
            return result != null && result.Contains('1');
        }
        private static void Exit()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Have a good day!");
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(0);
        }
    }
}
