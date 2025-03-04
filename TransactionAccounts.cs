using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class TransactionAccounts(string accountHolderName, double initBalance)
        : BankAccount(accountHolderName, initBalance)
    {
        public List<Transaction> Transactions { get;private set; }

        public ResponseCenter AddTransaction(Transaction tx)
        {
            if (tx.TypeInputOutputTx == TypeInOrOutTx.Increase)
            {

                Deposit(tx.TxPriceAmount);
            }
            else
            {
                Withdraw(tx.TxPriceAmount);
            }
        }


        public ResponseCenter GetTransactions()
        {

        }
    }
}
