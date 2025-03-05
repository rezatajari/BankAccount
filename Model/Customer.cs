using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankAccount.Model
{
    public class Customer
    {
        private static int _customerIdGenerator = 100;
        public Customer(string name, string email, double initBalance)
        {
            Name = name;
            Email = email;
            InitialBalance = initBalance;
            Id = ++_customerIdGenerator;
        }

        public double InitialBalance { get; private set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // List of bank account of a customer like (saving - loan or others)
        public List<Logic.BankService> Accounts { get; set; } = [];

        public List<Transaction> Transactions { get; set; } = [];
    }
}
