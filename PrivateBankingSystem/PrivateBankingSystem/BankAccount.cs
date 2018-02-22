using System;
using System.Collections.Generic;
using System.IO;


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
            return $"{LoggedUser}\t {Transaction}\t {AccountHolder}\t {Math.Round(Amount,2)}\t {Math.Round(Balance,2)}\t B{TransactionTimestamp}";
        }

        internal static List<BankAccount> statements = new List<BankAccount>();

        internal static void GetBalance(string username, bool isAdmin)
        {
            BankAccount statement = new BankAccount()
            {
                LoggedUser = username,
                Transaction = "Balance"
            };
            if (isAdmin)
            {
                string chooseAccount = ChooseAccount();
                if (chooseAccount == "1")
                {
                    Console.WriteLine("Enter the account holder's username");
                    string accountHolder = Console.ReadLine()?.ToLower().Trim();
                    bool accountHolderExist = DataBase.UsernameValidation(accountHolder);
                    if (accountHolderExist)
                    {
                        username = accountHolder;
                    }
                }
            }

            decimal balance = DataBase.GetBalance(username);
            
            statement.AccountHolder = username;
            statement.Balance = balance;
            statement.TransactionTimestamp = DateTime.Now;

            statements.Add(statement);
            
            Console.WriteLine($"Balance for {username} is {Math.Round(balance, 2)}\n");
            Helper.PressAnyKeyToContinue();
        }

        internal static void Deposit(string username, bool isAdmin)
        {
            BankAccount statement = new BankAccount()
            {
                LoggedUser = username,
                Transaction = "Deposit"
            };
            if (isAdmin)
            {
                string chooseAccount = ChooseAccount();
                if (chooseAccount == "1")
                {
                    Console.WriteLine("Enter the account holder's username");
                    string accountHolder = Console.ReadLine()?.ToLower().Trim();
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
                bool succesfullTransaction = DataBase.UpdateBalance(username, balance, timestamp);
                if (succesfullTransaction)
                {
                    statement.AccountHolder = username;
                    statement.Amount = deposit;
                    statement.Balance = balance;
                    statement.TransactionTimestamp = timestamp;

                    statements.Add(statement); 
                }
            }
            
            Helper.PressAnyKeyToContinue();
        }

        internal static void Withdrawal(string username, bool isAdmin)
        {
            BankAccount statement = new BankAccount()
            {
                LoggedUser = username,
                Transaction = "Withdrawal"
            };

            if (isAdmin)
            {
                string chooseAccount = ChooseAccount();
                if (chooseAccount == "1")
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

                    statement.AccountHolder = username;
                    statement.Amount = withdrawal;
                    statement.Balance = balance;
                    statement.TransactionTimestamp = timestamp;

                    statements.Add(statement);
                    
                }
                else
                {
                    Console.WriteLine("Insufficient balance");
                    Helper.PressAnyKeyToContinue();
                }
            }
        }

        internal static void GetStatement(string username, bool isAdmin)
        {
            using (StreamWriter statementFile = new StreamWriter(Helper.StatementFileName(username, DateTime.Today), true))
                foreach (BankAccount statement in statements)
                {
                    string[] line = new string[] { statement.LoggedUser, statement.Transaction, statement.AccountHolder, statement.Amount.ToString("C"), statement.Balance.ToString("C"), statement.TransactionTimestamp.ToLongDateString() };
                    statementFile.WriteLine(Format, line);
                }
            statements.Clear();
            using (StreamReader statementFile = new StreamReader(Helper.StatementFileName(username, DateTime.Today)))
            {
                while (!statementFile.EndOfStream)
                {
                    string getStatement = statementFile.ReadLine();
                    Console.WriteLine(getStatement);
                }
                Helper.PressAnyKeyToContinue();
            }

        }

        internal static void Exit(string username, bool isAdmin)
        {
            using (StreamWriter statementFile = new StreamWriter(Helper.StatementFileName(username, DateTime.Today), true))
            foreach (BankAccount statement in statements)
            {
                string[] line = new string[]{statement.LoggedUser, statement.Transaction, statement.AccountHolder, statement.Amount.ToString("C"), statement.Balance.ToString("C"), statement.TransactionTimestamp.ToLongTimeString()};
                statementFile.WriteLine(Format, line);
            }
            statements.Clear();
            Environment.Exit(0);
        }

        private const string Format = "{0, -12} {1, -12} {2, -15} {3, 13} {4, 13} {5, -30}";

        private static string ChooseAccount()
        {
            string chooseAccount = string.Empty;
            do
            {
                Console.WriteLine("My account (0) or other's account (1)");
                chooseAccount = Console.ReadLine();
            } while (chooseAccount != "0" && chooseAccount != "1");
            return chooseAccount;
        }
    }
}
