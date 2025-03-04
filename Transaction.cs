using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class Transaction
    {
        private static int _lastTxId = 100;
        public Transaction(string transactionName,double transactionPriceAmount)
        {
            TransactionId = ++_lastTxId; // Auto increment Tx ID
            TxPriceAmount = transactionPriceAmount;
            TxName = transactionName;
            DateOfTx = DateTime.UtcNow;
        }


        public int TransactionId { get;private set; }
        public string TxName { get; set; }
        public double TxPriceAmount { get; set; }
        public TypeOfTx TypeTx { get; private set; }  // Private because we don't need to access customer to set type of own transaction 
        public DateTime DateOfTx { get; set; }
        public TypeInOrOutTx TypeInputOutputTx { get; set; }
    }

    // For handle to increase or decrease money to own customer balance
    public enum TypeInOrOutTx
    {
        Increase,
        Decrease
    }

    // Type of model of transactions
    public enum TypeOfTx
    {
        Internal,
        TransferToOthers,
        BuySomething
    }
}
