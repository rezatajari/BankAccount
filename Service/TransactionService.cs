using BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankAccount.Service
{
    /// <summary>
    /// Transaction service is about tx services of customer, and we need to inject bank because bank service is need to process on customer account 
    /// </summary>
    /// <param name="bank"></param>
    public class TransactionService(BankService bank)
    {
        /// <summary>
        /// Create to new transaction from customer order
        /// </summary>
        /// <param name="accountId"></param>
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

            var whileFlag = true;
            while (whileFlag)
            {
                // Get user input
                Console.Write("Enter the number of your choice: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out var choice))
                {
                    // Convert choice to enum value
                    var selectTxType = (TypeOfTx)(choice - 1);

                    var tx = new Transaction(accountId);

                    switch (selectTxType)
                    {
                        case TypeOfTx.Deposit:
                            SetUpTransaction(name: "Deposit", TypeOfTx.Deposit);
                            break;
                        case TypeOfTx.Withdraw:
                            SetUpTransaction(name: "withdraw", TypeOfTx.Withdraw);
                            break;
                        case TypeOfTx.OutputTransferToOthers:
                            SetUpTransaction(name: "Output Transfer To Others", TypeOfTx.OutputTransferToOthers);
                            break;
                        case TypeOfTx.InputTransferFromOthers:
                            SetUpTransaction(name: "Input Transfer From Others", TypeOfTx.InputTransferFromOthers);
                            break;
                        case TypeOfTx.BuySomething:
                            Console.WriteLine("What does do you have buy it!? please enter the name\n");
                            var itemName = Console.ReadLine();
                            SetUpTransaction(itemName, TypeOfTx.BuySomething);
                            break;
                        default:
                            Console.WriteLine("Invalid your choice. please enter number between on the your display.");
                            break;
                    }

                    // Local function for proper transaction before to save
                    void SetUpTransaction(string name, TypeOfTx typeOfTx)
                    {
                        // Buy something
                        if (typeOfTx == TypeOfTx.BuySomething)
                        {
                            Console.WriteLine("How much for buy this!?\n");
                            var buyAmount = Console.ReadLine();
                            if (!string.IsNullOrEmpty(name) && double.TryParse(buyAmount, out var itemAmount))
                            {
                                tx.TxName = name;
                                tx.TxPriceAmount = itemAmount;
                                tx.TypeTx = TypeOfTx.BuySomething;
                            }
                            else
                            {
                                Console.WriteLine("Please input a name without empty input and should be correct amount input");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"How much do you want to {name} in your account\n");
                            // Other types tx
                            var inputAmount = Console.ReadLine();
                            if (double.TryParse(inputAmount, out var depositAmount))
                            {
                                tx.TxName = name;
                                tx.TxPriceAmount = depositAmount;
                                tx.TypeTx = typeOfTx;
                            }
                            else
                            {
                                Console.WriteLine("Your amount is should be correct amount.");
                            }
                        }
                    }

                    // Add transaction
                    var result = bank.AddTransaction(tx);

                    if (!result.IsValid)
                    {
                        Console.WriteLine(result.Message + "\n");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Your transaction is saved in your account\n");
                        Console.ResetColor();
                    }
                    whileFlag = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ResetColor();
                }
            }
        }
        /// <summary>
        /// Get list of transaction when a customer to order it
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<Transaction> GetTransactions(int accountId)
        {
            return bank.GetTransactions(accountId);
        }
    }
}
