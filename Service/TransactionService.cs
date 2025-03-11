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
            var newTx = new Transaction(accountId, "Apple", 100, TypeOfTx.BuySomething);
            var result=AddTransaction(newTx);
            if (!result.IsValid)
                Console.WriteLine(result.Message+"\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Your transaction is saved in your account\n");
            Console.ResetColor();
        }

        private ResponseCenter<bool> AddTransaction(Transaction tx)
        {
            var result = tx.TypeTx switch
            {
                TypeOfTx.Deposit => _bankService.Deposit(tx.AccountId,tx.TxPriceAmount),
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

        public List<Transaction> GetTransactions(int accountId)
        {
            return Transactions.Where(i => i.AccountId == accountId).ToList();
        }

    }
}
