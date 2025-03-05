using BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Logic
{
    public class TransactionService(BankService bankService)
    {
        public static List<Transaction> Transactions { get; private set; } = [];

        public ResponseCenter AddTransaction(Transaction tx)
        {
            var result = tx.TypeTx switch
            {
                TypeOfTx.Deposit => bankService.Deposit(tx.CustomerId,tx.TxPriceAmount),
                TypeOfTx.InputTransferFromOthers => bankService.Deposit(tx.CustomerId, tx.TxPriceAmount),
                TypeOfTx.BuySomething => bankService.Withdraw(tx.CustomerId, tx.TxPriceAmount),
                TypeOfTx.Withdraw => bankService.Withdraw(tx.CustomerId, tx.TxPriceAmount),
                TypeOfTx.OutputTransferToOthers => bankService.Withdraw(tx.CustomerId, tx.TxPriceAmount),
                _ => ResponseCenter.Fail("We should be regular transaction")
            };

            // Doesn't successful transaction
            if (!result.IsValid)
                return result;

            // Successful transaction
            Transactions.Add(tx);
            return result;
        }

        public List<Transaction> GetTransactions()
        {
            return Transactions;
        }



    }
}
