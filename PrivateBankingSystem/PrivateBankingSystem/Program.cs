using System;
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
                Environment.Exit(0);
            }
            
            user.IsAdmin = DataBase.IsAdmin(user.Username);
            
            UserTransaction userTransaction = null;

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
                    case UserChoice.Balance:
                        userTransaction = BankAccount.GetBalance;
                        break;
                    case UserChoice.GetTransaction:
                        break;
                    case UserChoice.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                userTransaction(user.Username, user.IsAdmin);

            } while (user.MenuChoice != UserChoice.Exit);
            
            

            

        }
    }

    
}
