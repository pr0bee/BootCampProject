using System;
using System.Text;
using System.Threading;

namespace PrivateBankingSystem
{
    class Menu
    {
        internal enum UserChoice
        {
            Withdrawal = 1,
            Deposit,
            Balance,
            GetTransaction,
            Exit
        }

        internal static UserChoice DisplayUserChoice(string username, bool isAdmin)
        {
            string userChoice = string.Empty;

            if (!isAdmin)
            {
                userChoiceList = userChoiceList.Remove(34, 14);
            }

            do
            {
                Console.Clear();
                Console.WriteLine($"Welcome {username} !\n" + "Main Menu");
                Console.WriteLine(userChoiceList);
                userChoice = Console.ReadLine()?.Trim();
                Console.Clear();
            } while (NotProperChoice(userChoice, isAdmin));
            return (UserChoice)Enum.Parse(typeof(UserChoice), userChoice);
        }

        // Checking that the user will make a proper choice.
        private static bool NotProperChoice(string userChoice, bool isAdmin)
        {
            bool inputIsInteger = int.TryParse(userChoice, out int intChoice);
            bool notProperChoice = false;

            if (inputIsInteger) 
            {
                if (isAdmin)
                {
                    notProperChoice = intChoice < 1 || intChoice > 5;
                }
                else
                {
                    notProperChoice = intChoice < 2 || intChoice > 5;
                }
            }
           return notProperChoice;
        }

        //private static UserChoice DisplayAdminChoice()
        //{
        //    string adminChoice = string.Empty;
        //    do
        //    {
        //        Console.WriteLine(userChoiceList);
        //        adminChoice = Console.ReadLine()?.Trim();
        //        Console.Clear();
        //    } while (CheckAdminChoice(adminChoice));
            
        //    return (UserChoice)Enum.Parse(typeof(UserChoice), adminChoice);
        //}

        //private static bool CheckAdminChoice(string adminChoice)
        //{
        //    bool inputIsInteger = int.TryParse(adminChoice, out int intChoice);
        //    bool choiceIsLegal = !inputIsInteger || intChoice < 1 || intChoice > 5;
        //    return choiceIsLegal;
        //}

        //private static void AdminMenu(string username)
        //{
        //    Console.WriteLine("Administrator Menu");

        //    switch (DisplayAdminChoice())
        //    {

        //        case UserChoice.Balance:

        //            Console.WriteLine("Choose : Your account(0), another user's account (1)");
        //            string getAccountChoice = Console.ReadLine();
        //            if (getAccountChoice == "0")
        //            {
        //                Transaction.Balance(username);
        //                Console.WriteLine($"Your account's balance is :{Math.Round(DataBase.GetBalance(username), 2)}");
        //                Thread.Sleep(5000);
        //                Console.Clear();
        //            }
        //            else
        //            {
        //                Console.WriteLine("Give the account owner's name");
        //                string accountOwner = Console.ReadLine();
        //                Console.WriteLine($"{accountOwner}'s account's balance is :{Math.Round(DataBase.GetBalance(accountOwner), 2)}");
        //                Thread.Sleep(5000);
        //                Console.Clear();
        //            }
        //            Thread.Sleep(5000);
        //            Console.Clear();
        //            break;

        //        case UserChoice.Deposit:

        //            string selection = String.Empty;
        //            do
        //            {
        //                Console.WriteLine("Deposit to my account (0)/n" + "Deposit to another account (1)/n" +
        //                                  "Return to main menu (2)");
        //                selection = Console.ReadLine();
        //                if (selection == "0")
        //                {
        //                    Transaction.Deposit(username);
        //                    Console.WriteLine(
        //                        $"Your account's balance is :{Math.Round(DataBase.GetBalance(username), 2)}");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Give the account owner's name");
        //                    string accountOwner = Console.ReadLine();
        //                    Transaction.Deposit(accountOwner);
        //                }
        //                Thread.Sleep(5000);
        //                Console.Clear();

                        
        //            } while (selection == "2");
        //            break;

        //        case UserChoice.Withdrawal:
        //            Console.WriteLine("You want to make a withdrawal from your(0) account or from someone else(1) account ?");
        //            getAccountChoice = Console.ReadLine();
        //            if (getAccountChoice == "0")
        //            {
        //                Transaction.Withdrawal(username);
        //                Console.WriteLine($"Your account's balance is :{Math.Round(DataBase.GetBalance(username), 2)}");
        //            }
        //            else
        //            {
        //                Console.WriteLine("Give the account owner's name");
        //                string accountOwner = Console.ReadLine();
        //                Transaction.Withdrawal(accountOwner);
        //            }
        //            Thread.Sleep(5000);
        //            Console.Clear();

        //            break;
        //        case UserChoice.GetTransaction:
        //            break;
        //        case UserChoice.Exit:
        //            Environment.Exit(0);
        //            break;

        //        default:

        //            break;
        //    }
        //}

        //private static void SimpleUserMenu(string username)
        //{
        //    Console.WriteLine("Simple User Menu\n" + $"Welcome {username}");

        //    UserChoice userChoise = DisplayUserChoice();

        //    // Looping the menu after each transaction till user choose to exit.
        //    while ( userChoise != UserChoice.Exit)                  
        //    {
        //        switch (userChoise)
        //        {

        //            case UserChoice.Balance:
        //                Transaction.Balance(username);
        //                Console.WriteLine($"Your account's balance is :{Math.Round(DataBase.GetBalance(username), 2)}");
        //                Thread.Sleep(5000);
        //                Console.Clear();
        //                userChoise = DisplayUserChoice();
        //                break;
        //            case UserChoice.Deposit:
        //                Console.WriteLine("You want to make a deposit to your(0) account or to someone else(1) account ?");
        //                string answer = Console.ReadLine();
        //                if (answer == "0")
        //                {
        //                    Transaction.Deposit(username);
        //                    goto case UserChoice.Balance;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Give the account owner's name");
        //                    string otherUser = Console.ReadLine();
        //                    Transaction.Deposit(otherUser);
        //                }
        //                Thread.Sleep(5000);
        //                Console.Clear();
        //                userChoise = DisplayUserChoice();
        //                break;
        //            case UserChoice.GetTransaction:
        //                break;
        //            case UserChoice.Exit:
        //                Console.Clear();
        //                Console.WriteLine("Bye");
        //                Thread.Sleep(2000);
        //                Environment.Exit(0);
        //                break;

        //            default:
        //                Console.WriteLine("Sorry, invalid selection");
        //                Thread.Sleep(3000);
        //                Console.Clear();
        //                userChoise = DisplayUserChoice();
        //                break;
        //        }
        //    }
                
            
        //}

        private static StringBuilder userChoiceList = new StringBuilder("Please choose from the list below\n" +
                                                         $"{UserChoice.Withdrawal} : { (int)UserChoice.Withdrawal}\n" +
                                                         $"{UserChoice.Deposit} : {(int)UserChoice.Deposit}\n" +
                                                         $"{UserChoice.Balance} : {(int)UserChoice.Balance}\n" +
                                                         $"{UserChoice.GetTransaction} : {(int)UserChoice.GetTransaction}\n" +
                                                         $"{UserChoice.Exit} : {(int)UserChoice.Exit}");




    }
}
