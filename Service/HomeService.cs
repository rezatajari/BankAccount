using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccount.Model;

namespace BankAccount.Service
{
    public class HomeService
    {
        public static void ApplicationRun()
        {
            Welcome();

            // --------------------- Creation account process --------------------- //

            if (!AccountCreate()) Exit();

            var bankService = new BankService();
            var result = bankService.CreateNewAccount();
            var accountId = result.Data;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result.Message + "\n");
            Console.ResetColor();

            // --------------------- Create transaction process --------------------- //

            if (!TransactionCreate()) Exit();
            var txService = new TransactionService(bankService);
            txService.CreateNewTx(accountId);

            // list of tx
            if(!GetTransaction()) Exit();
            var transactions = txService.GetTransactions();
            foreach (var tx in transactions)
            {
                Console.WriteLine("Name of your transaction: " + tx.TxName);
                Console.WriteLine("Amount of your transaction: " + tx.TxPriceAmount);
                Console.WriteLine("Date: " + tx.DateOfTx.Date);
                Console.WriteLine("Type of your transaction: " + tx.TypeTx);
            }

            // Check balance
            var checkBalance = CheckBalance();
            if (!checkBalance) Exit();

            bankService.CheckBalance(accountId);
        }

    

        private static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to bank center of american!:)\n");
            Console.ResetColor();
        }
        private static bool AccountCreate()
        {
            Console.WriteLine("Do you want to create new account!? 1)Yes 2)No (Exit!)\n");
            var result = Console.ReadLine();
            return result != null && result.Contains('1');
        }
        private static bool TransactionCreate()
        {
            Console.WriteLine("Do you have a new transaction? 1)Yes 2)No (Exit!)");
            var result = Console.ReadLine();
            return result != null && result.Contains('1');
        }
        private static bool GetTransaction()
        {
            Console.WriteLine("Do you want to get list of your transactions!? 1)Yes 2)No (Exit!)\n");
            var result = Console.ReadLine();
            return result != null && result.Contains('1');
        }
        private static bool CheckBalance()
        {
            Console.WriteLine("Do you want check your balance? 1)Yes 2)No (Exit)\n");
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
