using System;
using System.Text;

namespace PrivateBankingSystem
{
    internal enum UserChoice
    {
        Withdrawal = 1,
        Deposit,
        Balance,
        GetStatement,
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
                                                                        $"{UserChoice.GetStatement} : {(int) UserChoice.GetStatement}\n" +
                                                                        $"{UserChoice.Exit} : {(int) UserChoice.Exit}");

    }
}