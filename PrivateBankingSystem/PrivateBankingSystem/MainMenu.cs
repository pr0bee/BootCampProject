﻿using System;
using System.Text;
using System.Threading;

namespace PrivateBankingSystem
{
    internal enum UserChoice
    {
        Withdrawal = 1,
        Deposit,
        Transfer,
        Balance,
        GetStatement,
        Exit
    }

    class MainMenu
    {
        internal static UserChoice DisplayMainMenu(string username, bool isAdmin)
        {
            if (!isAdmin)
            {
                userChoiceList = simpleUserChoiceList;
            }

            int userChoice = 0;
            do
            {
                do
                {
                    Console.Clear();
                    Thread.Sleep(200);
                    Console.WriteLine($"Welcome {username} !\n" +
                                      $"\n" +
                                      $"Main Menu\n");
                    Console.WriteLine(userChoiceList);
                } while (!int.TryParse(Console.ReadLine(), out userChoice));
                
                Console.Clear();
            } while (NotProperChoice(userChoice, isAdmin));
            return (UserChoice) Enum.Parse(typeof(UserChoice), userChoice.ToString());
        }

        // Checking that the user's choice against the list.
        private static bool NotProperChoice(int userChoice, bool isAdmin)
        {
            bool notProperChoice = false;

                if (isAdmin)
                {
                    notProperChoice = userChoice < (int)UserChoice.Withdrawal || userChoice > (int)UserChoice.Exit;
                }
                else
                {
                    notProperChoice = userChoice < (int)UserChoice.Deposit || userChoice > (int)UserChoice.Exit;
                }
            return notProperChoice;
        }


        private static StringBuilder userChoiceList = new StringBuilder($"Please choose from the list below :\n" +
                                                                        $"\n" +
                                                                        $"{(int) UserChoice.Withdrawal} : {UserChoice.Withdrawal}\n" +
                                                                        $"{(int) UserChoice.Deposit} : {UserChoice.Deposit}\n" +
                                                                        $"{(int) UserChoice.Transfer} : {UserChoice.Transfer}\n" +
                                                                        $"{(int) UserChoice.Balance} : {UserChoice.Balance}\n" +
                                                                        $"{(int) UserChoice.GetStatement} : {UserChoice.GetStatement}\n" +
                                                                        $"{(int) UserChoice.Exit} : {UserChoice.Exit}");

        private static StringBuilder simpleUserChoiceList = new StringBuilder($"Please choose from the list below :\n" +
                                                                              $"\n" +
                                                                              $"{(int)UserChoice.Deposit} : {UserChoice.Deposit}\n" +
                                                                              $"{(int)UserChoice.Transfer} : {UserChoice.Transfer}\n" +
                                                                              $"{(int)UserChoice.Balance} : {UserChoice.Balance}\n" +
                                                                              $"{(int)UserChoice.GetStatement} : {UserChoice.GetStatement}\n" +
                                                                              $"{(int)UserChoice.Exit} : {UserChoice.Exit}");
    }
}