using System;
using System.Diagnostics;
using System.Text;

namespace PrivateBankingSystem
{
    internal enum UserChoice
    {
        Withdrawal = 1,
        Deposit,
        Balance,
        GetTransaction,
        Exit
    }

    class MainMenu
    {
        

        internal static UserChoice DisplayMainMenu(string username, bool isAdmin)
        {

            // Removes Withdrawal choice for simple users.
            if (!isAdmin)
            {
                userChoiceList = userChoiceList.Remove(34, 14);
            }

            string userChoice = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine($"Welcome {username} !\n\b" + "Main Menu");
                Console.WriteLine(userChoiceList);
                userChoice = Console.ReadLine();
                Console.Clear();
            } while (NotProperChoice(userChoice, isAdmin));
            return (UserChoice) Enum.Parse(typeof(UserChoice), userChoice);
        }

        // Checking that the user will choose from the list.
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


        private static StringBuilder userChoiceList = new StringBuilder("Please choose from the list below\n" +
                                                                        $"{UserChoice.Withdrawal} : {(int) UserChoice.Withdrawal}\n" +
                                                                        $"{UserChoice.Deposit} : {(int) UserChoice.Deposit}\n" +
                                                                        $"{UserChoice.Balance} : {(int) UserChoice.Balance}\n" +
                                                                        $"{UserChoice.GetTransaction} : {(int) UserChoice.GetTransaction}\n" +
                                                                        $"{UserChoice.Exit} : {(int) UserChoice.Exit}");

    }
}

//internal static void Display(string username, bool isAdmin)
        //{
        //    Console.WriteLine("Transaction Menu");

        //    switch (DisplayMainMenu(username, isAdmin))
        //    {

        //        case UserChoice.Balance:

        //            if (isAdmin)
        //            {

        //            }

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

    

