using System;

namespace PrivateBankingSystem
{
    class LoginScreen
    {
        internal static string Username()
        {
            Console.WriteLine("Enter your username : ");
            return Console.ReadLine().ToLower().Trim();
        }

        internal static string Password()
        {
            Console.WriteLine("Enter your password : ");
            return Helper.HidePassword();
        }
    }
}
