using System;
using System.Threading;
using System.Data.SqlClient;

namespace PrivateBankingSystem
{
    class DataBase
    {
        static SqlConnection sqlConn = new SqlConnection(Helper.ConVal("BankingDB"));

        internal static void CheckServerConnection()
        {
            try
            {
                sqlConn.Open();
                sqlConn.Close();
            }
            catch (SqlException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Connection to Server lost. Please try later.");
                Thread.Sleep(5000);
                Console.ResetColor();
                Environment.Exit(0);
            }
        }
        
        internal static bool CredentialsValidation(string username, string password)
        {
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) " +
                                                "FROM users " +
                                                "WHERE username = @username AND password = @password", sqlConn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                
                int result = (int)cmd.ExecuteScalar();
                sqlConn.Close();

                if (result > 0)
                {
                    Console.WriteLine("Login Successfull");
                    return true;
                }
                else
                {
                    Console.WriteLine("Login Failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            //finally
            //{
            //    Console.ReadKey();
            //}
        }

        internal static decimal GetBalance(string username)
        {
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("SELECT accounts.amount, users.username " +
                                                "FROM accounts, users " +
                                                "WHERE accounts.user_id = users.id AND users.username = @username", sqlConn);
                cmd.Parameters.AddWithValue("@username", username);
                decimal balance = (decimal)cmd.ExecuteScalar();
                sqlConn.Close();
                return balance;
                //Console.WriteLine($"Your Account's Balance is : {Math.Round(result,2)} ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        internal static void UpdateBalance(string username, decimal newBalance, DateTime timestamp)
        {
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE accounts " +
                                                "SET amount = @newBalance, " +
                                                "transaction_date = @timestamp " +
                                                "WHERE accounts.user_id IN (SELECT accounts.user_id from accounts, users " +
                                                                            "WHERE accounts.user_id = users.id AND users.username = @username)", sqlConn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@newBalance", newBalance);
                cmd.Parameters.AddWithValue("@timestamp", timestamp);
                int result = cmd.ExecuteNonQuery();
                sqlConn.Close();

                if (result != 0)
                {
                    Console.WriteLine("Transaction completed successfully");
                }
                else
                {
                    Console.WriteLine("There was no transaction");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }




    }
}
