using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class Customer(string name, string email)
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;

        // List of bank account of a customer like (saving - loan or others)
        public List<BankAccount> Accounts { get; set; } = [];
    }
}
