using System;
using System.IO;
using System.Threading;


namespace PrivateBankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase.CheckServerConnection();
            
            User user = new User();
            bool succesfullLogin = false;
            int logginAttempts = 0;

            do
            {
                Console.WriteLine("Please login\n");
                Console.WriteLine($"You have {3 - logginAttempts} attempt(s) left\n");
                user.Username = LoginScreen.Username();
                user.Password = LoginScreen.Password();

                string cipherPassword = Helper.CipherPassword(user.Password);

                succesfullLogin = DataBase.CredentialsValidation(user.Username, cipherPassword);

                logginAttempts++;

                Thread.Sleep(2000);
                Console.Clear();
            } while (!succesfullLogin && logginAttempts < 3);

            if (!succesfullLogin)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Please check your username and password, then try again later.");
                Thread.Sleep(2500);
                Environment.Exit(0);
            }

            Helper.CreateStatementFile(user.Username);
            
            user.IsAdmin = DataBase.IsAdmin(user.Username);
            
            UserTransaction userTransaction;

            do
            {
                user.MenuChoice = MainMenu.DisplayMainMenu(user.Username, user.IsAdmin);
                switch (user.MenuChoice)
                {
                    case UserChoice.Withdrawal:
                        userTransaction = BankAccount.Withdrawal;
                        break;
                    case UserChoice.Deposit:
                        userTransaction = BankAccount.Deposit;
                        break;
                    case UserChoice.Transfer:
                        userTransaction = BankAccount.Transfer;
                        break;
                    case UserChoice.Balance:
                        userTransaction = BankAccount.GetBalance;
                        break;
                    case UserChoice.GetStatement:
                        userTransaction = BankAccount.GetStatement;
                        break;
                    case UserChoice.Exit:
                        userTransaction = BankAccount.Exit;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                userTransaction(user.Username, user.IsAdmin);
            } while (user.MenuChoice != UserChoice.Exit);
            
         
        }
    }
   
}
