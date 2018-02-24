using System;

namespace PrivateBankingSystem
{
    class LoginScreen
    {
        internal static string Username()
        {
            Console.Write("Enter your username : ");
            return Console.ReadLine().ToLower().Trim();
        }

        internal static string Password()
        {
            Console.Write("Enter your password : ");
            return Helper.HidePassword();
        }
    }
}
