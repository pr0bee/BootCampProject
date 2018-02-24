using System;
using System.Threading;
using System.Data.SqlClient;
using System.Data.Linq;

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
                Helper.PressAnyKeyToContinue();
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

                int result = (int) cmd.ExecuteScalar();
                sqlConn.Close();

                if (result > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Login Successfull");
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Login Failed");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }

        internal static decimal GetBalance(string username)
        {
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("SELECT accounts.amount, users.username " +
                                                "FROM accounts, users " +
                                                "WHERE accounts.user_id = users.id AND users.username = @username",sqlConn);
                cmd.Parameters.AddWithValue("@username", username);
                decimal balance = (decimal) cmd.ExecuteScalar();
                sqlConn.Close();
                return balance;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        internal static bool UpdateBalance(string username, decimal newBalance, DateTime timestamp)
        {
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE accounts " +
                                                "SET amount = @newBalance, " +
                                                "transaction_date = @timestamp " +
                                                "WHERE accounts.user_id IN (SELECT accounts.user_id from accounts, users " +
                                                "WHERE accounts.user_id = users.id AND users.username = @username)",sqlConn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@newBalance", newBalance);
                cmd.Parameters.AddWithValue("@timestamp", timestamp);
                int result = cmd.ExecuteNonQuery();
                sqlConn.Close();

                if (result != 0)
                {
                    Console.WriteLine("Transaction completed successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine("There was no transaction");
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        internal static bool IsAdmin(string username)
        {
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("SELECT id FROM users WHERE username = @username", sqlConn);
                cmd.Parameters.AddWithValue("@username", username);
                int id = (int) cmd.ExecuteScalar();
                sqlConn.Close();
                return (id == 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        internal static bool UsernameValidation(string username)
        {
            try
            {
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) " +
                                                "FROM users " +
                                                "WHERE username = @username", sqlConn);
                cmd.Parameters.AddWithValue("@username", username);

                int result = (int) cmd.ExecuteScalar();
                sqlConn.Close();

                if (result > 0)
                {
                    Console.WriteLine("The username is valid");
                    return true;
                }
                else
                {
                    Console.WriteLine("The username is not valid");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
