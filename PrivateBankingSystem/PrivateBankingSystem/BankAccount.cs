using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
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


        DateTimeFormatInfo myDTFI = new CultureInfo("el-GR", false).DateTimeFormat;
        
        public override string ToString()
        {
            return $"{LoggedUser}\t {Transaction}\t {AccountHolder}\t {Math.Round(Amount,2)}\t" +
                   $" {Math.Round(Balance,2)}\t {TransactionTimestamp.ToString(myDTFI.UniversalSortableDateTimePattern)}";
        }

        private static List<BankAccount> statements = new List<BankAccount>();

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
                    username = NewAccountHolder(username);
                }
            }

            decimal balance = DataBase.GetBalance(username);

            decimal withdrawal = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter the ammount of withdrawal");
            } while (!decimal.TryParse(Console.ReadLine(), out withdrawal));

            if (AmmountConfirmation(withdrawal))
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

        internal static void Deposit(string username, bool isAdmin)
        {
            BankAccount statement = new BankAccount()
            {
                LoggedUser = username,
                Transaction = "Deposit"
            };

            decimal balance = DataBase.GetBalance(username);

            string chooseAccount = ChooseAccount();
            if (chooseAccount == "1")
            {
                username = NewAccountHolder(username);
            }
            
            decimal deposit = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter the ammount of deposit");
            }
            while (!decimal.TryParse(Console.ReadLine(), out deposit));

            if (AmmountConfirmation(deposit))
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

        internal static void Transfer(string username, bool isAdmin)
        {
            BankAccount statement = new BankAccount()
            {
                LoggedUser = username,
                Transaction = "Transfer to",
            };

            string beneficiary = NewAccountHolder(username);

            decimal balance = DataBase.GetBalance(username);

            decimal amountToTransfer = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter the ammount to transfer");
            } while (!decimal.TryParse(Console.ReadLine(), out amountToTransfer));

            if (!AmmountConfirmation(amountToTransfer))
            {
                return;
            }

            if (amountToTransfer <= balance)
            {
                Console.WriteLine($"You transfered {Math.Round(amountToTransfer,2)} to {beneficiary}");
                balance -= amountToTransfer;
                DateTime timestamp = DateTime.Now;
                DataBase.UpdateBalance(username, balance, timestamp);

                decimal balanceOfBeneficiary = DataBase.GetBalance(beneficiary);
                balanceOfBeneficiary += amountToTransfer;
                DataBase.UpdateBalance(beneficiary, balanceOfBeneficiary, timestamp);

                Helper.PressAnyKeyToContinue();

                statement.AccountHolder = beneficiary;
                statement.Amount = amountToTransfer;
                statement.Balance = balance;
                statement.TransactionTimestamp = timestamp;

                statements.Add(statement);
                FillStatement(username);

                BankAccount statementOfBeneficiary = new BankAccount()
                {
                    LoggedUser = beneficiary,
                    Transaction = "Transfer from",
                    AccountHolder = username,
                    Amount = amountToTransfer,
                    Balance = balanceOfBeneficiary,
                    TransactionTimestamp = timestamp
                };
                    
                Helper.CreateStatementFile(beneficiary);
                statements.Add(statementOfBeneficiary);
                FillStatement(beneficiary);
            }
            else
            {
                Console.WriteLine("Insufficient balance");
                Helper.PressAnyKeyToContinue();
            }
        }

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
                    username = NewAccountHolder(username); 
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

        internal static void GetStatement(string username, bool isAdmin)
        {
            FillStatement(username);

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
            FillStatement(username);
            Environment.Exit(0);
        }

        private static string ChooseAccount()
        {
            string chooseAccount = string.Empty;
            bool inputIsFromBlackList ;
            do
            {
                Console.WriteLine("My account (0) or other's account (1)");
                chooseAccount = Console.ReadLine();
                inputIsFromBlackList = (chooseAccount != "0" & chooseAccount != "1") | string.IsNullOrEmpty(chooseAccount);
            } while (inputIsFromBlackList);
            return chooseAccount;
        }

        private static string NewAccountHolder(string username)
        {
            bool accountHolderExist = false;
            string newAccountHolder = String.Empty;
            do
            {
                do
                {
                    Console.WriteLine("Enter the account holder's username");
                    newAccountHolder = Console.ReadLine()?.ToLower().Trim(); 
                }
                while (newAccountHolder == username);
                
                accountHolderExist = DataBase.UsernameValidation(newAccountHolder);
            }
            while (!accountHolderExist);
            return newAccountHolder;
        }

        private static bool AmmountConfirmation(decimal amount)
        {
            string answer = String.Empty;
            bool answerIsNotAccepted = false;

            do
            {
                Console.Clear();
                Console.WriteLine($"You entered : {Math.Round(amount, 2)} ? y/n");
                answer = Console.ReadLine().ToLower().Trim();
                answerIsNotAccepted = (answer != "y" & answer != "n") | string.IsNullOrEmpty(answer);
            } while (answerIsNotAccepted);

            bool amountConfirmed = answer == "y";
            return amountConfirmed;
        }

        // Formating the statement file.
        internal const string Format = "{0, -12} {1, -16} {2, -15} {3, 13} {4, 13} {5, -50}";

        private static void FillStatement(string username)
        {
            using (StreamWriter statementFile = new StreamWriter(Helper.StatementFileName(username, DateTime.Today), true))
                foreach (BankAccount statement in statements)
                {
                    string[] line = new string[] { statement.LoggedUser, statement.Transaction, statement.AccountHolder, statement.Amount.ToString("C"), statement.Balance.ToString("C"), statement.TransactionTimestamp.ToLongTimeString() };
                    statementFile.WriteLine(Format, line);
                }
            statements.Clear();
        }

    }
}
