using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BankAccount.Model;

namespace BankAccount.Service
{
    public class HomeService
    {
        public static void ApplicationRun()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to bank center of american!:)\n");
            Console.ResetColor();

            var bank = new BankService();
            var accountId = AccountCreated(bank);

            // After an account created by a customer
            ApplicationMenu(bank,accountId);
        }

        private static int AccountCreated(BankService bank)
        {
            while (true)
            {
                Console.WriteLine("Do you want to create new account!? 1)Yes 2)No (Exit!)\n");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var response))
                {
                    switch (response)
                    {
                        case 1:
                            var result = bank.CreateNewAccount();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(result.Message + "\n");
                            Console.ResetColor();
                            return result.Data;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid your number input, please enter a number between 1 and 2.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. please enter a number.");
                }
            }
        }
        private static void ApplicationMenu(BankService bank,int accountId)
        {
            while (true)
            {
                Console.WriteLine("What do you want to do!? 1)New transaction create? 2)Check your balance 3)List of your transactions 4)Exit");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var bankMenu))
                {
                    switch (bankMenu)
                    {
                        case 1:
                            NewTransaction(accountId);
                            break;
                        case 2:
                            CheckCustomerBalance(bank,accountId);
                            break;
                        case 3:
                            GetListTransaction(accountId);
                            break;
                        case 4:
                            break;
                        default:
                            Console.WriteLine("Invalid your number input, please enter a number between 1 and 4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. please enter a number.");
                }
            }
        }
        private static void NewTransaction(int accountId)
        {
            while (true)
            {
                Console.WriteLine("Do you have a new transaction? 1)Yes 2)No (Exit!)");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var result))
                {
                    switch (result)
                    {
                        case 1:
                            var txService = new TransactionService();
                            txService.CreateNewTx(accountId);
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid number please enter number between 1 and 2.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. please input only number.");
                }
            }
        }
        private static void CheckCustomerBalance(BankService bank, int accountId)
        {
            while (true)
            {
                Console.WriteLine("Do you want check your balance? 1)Yes 2)No (Exit)\n");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var result))
                {
                    switch (result)
                    {
                        case 1:
                            bank.CheckBalance(accountId);
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid input. please enter number between 1 and 2.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. please enter only number");
                }
            }
        }
        private static void GetListTransaction(int accountId)
        {
            while (true)
            {
                Console.WriteLine("Do you want to get list of your transactions!? 1)Yes 2)No (Exit!)\n");
                var input = Console.ReadLine();
                if (int.TryParse(input, out var result))
                {
                    switch (result)
                    {
                        case 1:
                            var transactionService = new TransactionService();
                            var listOfTx=    transactionService.GetTransactions(accountId);
                            foreach (var tx in listOfTx)
                            {
                                Console.WriteLine("Name of your transaction: " + tx.TxName);
                                Console.WriteLine("Amount of your transaction: " + tx.TxPriceAmount);
                                Console.WriteLine("Date: " + tx.DateOfTx.Date);
                                Console.WriteLine("Type of your transaction: " + tx.TypeTx);
                            }
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid number. please enter between 1 and 2.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. please enter only number.");
                }
            }
        }
    }
}
