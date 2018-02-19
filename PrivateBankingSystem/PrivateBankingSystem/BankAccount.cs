using System;
using System.Threading;

namespace PrivateBankingSystem
{
    public delegate void UserTransaction(string username, bool isAdmin);

    class BankAccount
    {
        public string LoggedUser { get; set; }
        public string AccountHolder { get; set; }
        public string Transaction { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime TransactionTimestamp { get; set; }

        public override string ToString()
        {
            return $"Logged user : {LoggedUser}, transaction type : {Transaction}, account holder : {AccountHolder}, amount : {Amount}, account balance : {Balance}, transaction date : {TransactionTimestamp}";
        }

        //List<BankAccount> transactions = new List<BankAccount>();






        internal static void GetBalance(string username, bool isAdmin)
        {
            BankAccount transaction = new BankAccount()
            {
                LoggedUser = username,
                Transaction = "Balance",
            };
            if (isAdmin)
            {
                Console.WriteLine("Check my account (0) or another user (1)");
                string chooseAccountHolder = Console.ReadLine();
                if (chooseAccountHolder == "1")
                {
                    Console.WriteLine("Enter the account holder's username");
                    string accountHolder = Console.ReadLine().ToLower().Trim();
                    bool accountHolderExist = DataBase.UsernameValidation(accountHolder);
                    if (accountHolderExist)
                    {
                        username = accountHolder;
                    }
                }
                
            }

            decimal balance = DataBase.GetBalance(username);
            
            transaction.AccountHolder = username;
            transaction.Balance = balance;
            transaction.TransactionTimestamp = DateTime.Now;
            

            Console.WriteLine($"Balance for {username} is {Math.Round(balance, 2)}\n");
            Helper.PressAnyKeyToContinue();
            
        }

        
        internal static void Deposit(string username, bool isAdmin)
        {
            if (isAdmin)
            {
                Console.WriteLine("Deposit to my account (0) or to another user's account (1)");
                string chooseAccountHolder = Console.ReadLine();
                if (chooseAccountHolder == "1")
                {
                    Console.WriteLine("Enter the account holder's username");
                    string accountHolder = Console.ReadLine().ToLower().Trim();
                    bool accountHolderExist = DataBase.UsernameValidation(accountHolder);
                    if (accountHolderExist)
                    {
                        username = accountHolder;
                    }
                }


            }

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
            if (isAdmin)
            {
                Console.WriteLine("Withdraw from my account (0) or from another user's account (1)");
                string chooseAccountHolder = Console.ReadLine();
                if (chooseAccountHolder == "1")
                {
                    Console.WriteLine("Enter the account holder's username");
                    string accountHolder = Console.ReadLine().ToLower().Trim();
                    bool accountHolderExist = DataBase.UsernameValidation(accountHolder);
                    if (accountHolderExist)
                    {
                        username = accountHolder;
                    }
                }

            }
            

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
                    Helper.PressAnyKeyToContinue();
                }
                else
                {
                    Console.WriteLine("Insufficient balance");
                    Helper.PressAnyKeyToContinue();
                }

            }
        }
    }
}
