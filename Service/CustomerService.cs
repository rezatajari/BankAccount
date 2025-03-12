using BankAccount.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Service
{
    public class CustomerService
    {
        public Customer CreateCustomerInformation()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Please enter your information:\n");
            Console.ResetColor();
            var name = GetCustomerName();
            var email = GetCustomerEmail();
            var initBalance = GetCustomerInitBalance();
            return new Customer(name, email, initBalance);
        }

        private static double GetCustomerInitBalance()
        {

            Console.WriteLine("Please enter your initial balance:\n");
            var isValid = false;
            double initBalance;
            do
            {
                if (double.TryParse(Console.ReadLine(), out initBalance))
                    isValid = true;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter correct balance\n");
                    Console.ResetColor();
                }

            } while (!isValid);

            return initBalance;
        }

        private static string GetCustomerName()
        {
            var isValid = false;
            string name;
            Console.WriteLine("Enter your name:\n");
            do
            {
                name = Console.ReadLine();
                isValid = StringValidation(name, CustomerFieldsNames.Name);

            } while (!isValid);

            return name;
        }

        private static string GetCustomerEmail()
        {
            var isValid = false;
            string email;
            Console.WriteLine("Please enter your email:\n");
            do
            {
                email = Console.ReadLine();
                isValid = StringValidation(email, CustomerFieldsNames.Email);

            } while (!isValid);

            return email;
        }

        private enum CustomerFieldsNames
        {
            Name,
            Email
        }
        private static bool StringValidation(string stringType, CustomerFieldsNames fieldName)
        {
            var input = fieldName switch
            {
                CustomerFieldsNames.Name => "Name",
                CustomerFieldsNames.Email => "Email",
                _ => "Your input"
            };

            if (!string.IsNullOrEmpty(stringType)) return true;

            Console.WriteLine($"{input} cannot null or empty\n");
            return false;

        }
    }
}
