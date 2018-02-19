using System;
using System.Threading;

namespace PrivateBankingSystem
{
    

    class Transaction
    {
        public delegate void myTransaction(string username, bool isAdmin);

        internal static void Balance(string username, bool isAdmin)
        {
            decimal balance = DataBase.GetBalance(username);
            Console.WriteLine(username + " " + Math.Round(balance, 2));
            Thread.Sleep(5000);
        }

        
        internal static void Deposit(string username, bool isAdmin)
        {
            decimal balance = DataBase.GetBalance(username);
            Console.WriteLine("Enter the ammount of deposit");
            decimal deposit = decimal.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine($"You entered : {deposit} ? y/n");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                balance += deposit;
                DateTime timestamp = DateTime.Now;
                DataBase.UpdateBalance(username, balance, timestamp);
                
            }
            
        }

        internal static void Withdrawal(string username, bool isAdmin)
        {
            decimal balance = DataBase.GetBalance(username);
            Console.WriteLine("Enter the ammount of withdrawal");
            bool checkInput = decimal.TryParse(Console.ReadLine(), out decimal withdrawal);
            
            Console.Clear();
            Console.WriteLine($"You entered : {withdrawal} ? y/n");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                if (withdrawal <= balance)
                {
                    Console.WriteLine($"You have withdraw {withdrawal}");
                    balance -= withdrawal;
                    DateTime timestamp = DateTime.Now;
                    DataBase.UpdateBalance(username, balance, timestamp);
                }
                else
                {
                    Console.WriteLine("Insufficient balance");
                }

            }
        }
    }
}
