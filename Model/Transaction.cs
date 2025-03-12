using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Model
{
    public class Transaction
    {
        private static int _lastTxId = 100;
        public Transaction(int accountId)
        {
            Id = _lastTxId++; // Auto increment Tx ID
            DateOfTx = DateTime.UtcNow;
            AccountId = accountId;
        }


        public int Id{ get;private set; }
        public string TxName { get; set; }
        public double TxPriceAmount { get; set; }
        public TypeOfTx TypeTx { get;  set; }  
        public DateTime DateOfTx { get; set; }
        public int AccountId { get; set; }
       
    }


    // Type of model of transactions
    public enum TypeOfTx
    {
        Deposit,
        Withdraw,
        OutputTransferToOthers,
        InputTransferFromOthers,
        BuySomething
    }

}
