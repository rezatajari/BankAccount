using BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Service
{
    public class TransactionService
    {
        private readonly BankService _bankService = new();
        public static List<Transaction> Transactions { get; private set; } = [];

        public void CreateNewTx(int accountId)
        {
           
            // Display available transaction types
            Console.WriteLine("What type of transaction do you want to add?");
            var index = 1;
            foreach (var typeOfTx in Enum.GetValues(typeof(TypeOfTx)))
            {
                Console.WriteLine($"{index}) {typeOfTx}");
                index++;
            }

            while (true)
            {

                // Get user input
                Console.Write("Enter the number of your choice: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out var choice))
                {
                    string transactionName="";
                    double transactionPriceAmount=0;
                    TypeOfTx typeTx=0;

                    // Convert choice to enum value
                    var selectTxType = (TypeOfTx)(choice - 1); // Subtract 1 because enums are zero-b
                   
                    string inputAmount;
                    switch (selectTxType)
                    {
                        case TypeOfTx.Deposit:
                            Console.WriteLine("How much do you want deposit in your account!?\n");
                            inputAmount = Console.ReadLine();
                            if (double.TryParse(inputAmount, out var depositAmount))
                            {
                                transactionName = "Deposit";
                                transactionPriceAmount = depositAmount;
                                typeTx = TypeOfTx.Deposit;
                            }
                            else
                            {
                                Console.WriteLine("Your amount is should be correct amount.");
                            }

                            break;
                        case TypeOfTx.Withdraw:
                            Console.WriteLine("How much do you want withdraw in your account!?\n");
                            inputAmount = Console.ReadLine();
                            if (double.TryParse(inputAmount, out var withdrawAmount))
                            {
                                transactionName = "Withdraw";
                                transactionPriceAmount = withdrawAmount;
                                typeTx = TypeOfTx.Withdraw;
                            }
                            else
                            {
                                Console.WriteLine("Your amount is should be correct amount.");
                            }

                            break;
                        case TypeOfTx.OutputTransferToOthers:
                            Console.WriteLine("How much do you want send to other account from your account!?\n");
                            inputAmount = Console.ReadLine();
                            if (double.TryParse(inputAmount, out var outPutAmount))
                            {
                                transactionName = "Output Transfer To Others";
                                transactionPriceAmount = outPutAmount;
                                typeTx= TypeOfTx.OutputTransferToOthers;
                            }
                            else
                            {
                                Console.WriteLine("Your amount is should be correct amount.");
                            }

                            break;
                        case TypeOfTx.InputTransferFromOthers:
                            Console.WriteLine("How much receive input to your account from other!? \n");
                            inputAmount = Console.ReadLine();
                            if (double.TryParse(inputAmount, out var inputFromOtherAmount))
                            {
                                transactionName = "Input Transfer From Others";
                                transactionPriceAmount = inputFromOtherAmount;
                                typeTx = TypeOfTx.InputTransferFromOthers;
                            }
                            else
                            {
                                Console.WriteLine("Your amount is should be correct amount.");
                            }

                            break;
                        case TypeOfTx.BuySomething:
                            Console.WriteLine("What does do you have buy it!? please enter the name\n");
                            var itemName= Console.ReadLine();
                            Console.WriteLine("How much for buy this!?\n");
                            inputAmount = Console.ReadLine();

                            if (string.IsNullOrEmpty(itemName) && double.TryParse(inputAmount,out var itemAmount))
                            {
                                transactionName = itemName;
                                transactionPriceAmount = itemAmount;
                                typeTx = TypeOfTx.BuySomething;
                            }
                            else
                            {
                                Console.WriteLine("Please input a name without empty input and should be correct amount input");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid your choice. please enter number between on the your display.");
                            break;
                    }

                    var newTx = new Transaction(accountId, transactionName, transactionPriceAmount, typeTx);
                    var result = AddTransaction(newTx);

                    if (!result.IsValid)
                        Console.WriteLine(result.Message + "\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Your transaction is saved in your account\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        public List<Transaction> GetTransactions(int accountId)
        {
            return Transactions.Where(i => i.AccountId == accountId).ToList();
        }
        private ResponseCenter<bool> AddTransaction(Transaction tx)
        {
            var result = tx.TypeTx switch
            {
                TypeOfTx.Deposit => _bankService.Deposit(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.InputTransferFromOthers => _bankService.Deposit(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.BuySomething => _bankService.Withdraw(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.Withdraw => _bankService.Withdraw(tx.AccountId, tx.TxPriceAmount),
                TypeOfTx.OutputTransferToOthers => _bankService.Withdraw(tx.AccountId, tx.TxPriceAmount),
                _ => ResponseCenter<bool>.Fail("We should be regular transaction")
            };

            // Doesn't successful transaction
            if (!result.IsValid)
                return result;

            // Successful transaction
            Transactions.Add(tx);
            return result;
        }


    }
}
