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
            
            while (!succesfullLogin && logginAttempts < 4)
            {
                logginAttempts++;
                if (logginAttempts == 4)
                {
                    Environment.Exit(0);
                }
                Console.WriteLine("Please login");
                Console.WriteLine($"You have {4 - logginAttempts} attempt(s) left");
                user.Username = LoginScreen.Username();
                user.Password = LoginScreen.Password();
                
                string cipherPassword = Helper.CipherPassword(user.Password);
                
                succesfullLogin = DataBase.CredentialsValidation(user.Username, cipherPassword);
                
                Thread.Sleep(2000);
                Console.Clear();
            }

            Menu.Display(user.Username, succesfullLogin);

            Environment.Exit(0);

        }
    }
}
