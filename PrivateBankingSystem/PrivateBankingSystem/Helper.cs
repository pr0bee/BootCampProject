using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PrivateBankingSystem
{
    class Helper
    {

        internal static string HidePassword()
        {
            string password = String.Empty;
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                    info = Console.ReadKey(true);
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (info.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    info = Console.ReadKey(true);
                }
            }
            return password.ToLower().Trim();
        }

        internal static string ConVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
        
        private static byte[] AES_Encrypt(byte[] passwordBytes, byte[] passPhraseBytes)
        {
            // Source : https://www.codeproject.com/Articles/769741/Csharp-AES-bits-Encryption-Library-with-Salt .
            byte[] encryptedPasswordBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 10, 65, 44, 128, 10, 96, 88, 76, 32, 12, 66, 92 };

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (RijndaelManaged myAES = new RijndaelManaged())
                {
                    myAES.KeySize = 256;
                    myAES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passPhraseBytes, saltBytes, 1000);
                    myAES.Key = key.GetBytes(myAES.KeySize / 8);
                    myAES.IV = key.GetBytes(myAES.BlockSize / 8);

                    myAES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(memoryStream, myAES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(passwordBytes, 0, passwordBytes.Length);
                        cs.Close();
                    }
                    encryptedPasswordBytes = memoryStream.ToArray();
                }
            }

            return encryptedPasswordBytes;
        }

        internal static string CipherPassword(string password)
        {
            // Get the bytes of the string.
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            
            byte[] passPhraseBytes = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings.Get("passPhrase"));

            // Hash the password with SHA256
            passPhraseBytes = SHA256.Create().ComputeHash(passPhraseBytes);

            byte[] encryptedPasswordBytes = AES_Encrypt(passwordBytes, passPhraseBytes);

            string cipherPassword = Convert.ToBase64String(encryptedPasswordBytes);

            return cipherPassword;
        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        internal static String StatementFileName(string username, DateTime date)
        {
            const string path = @"D:\Dropbox\_BC3\GitHub\BootCampProject\PrivateBankingSystem\";
            return $"{path}statement_{username}_{date.Day}_{date.Month}_{date.Year}.txt";
        }

        internal static void CreateStatementFile(string username)
        {
            string statmentFileName = Helper.StatementFileName(username, DateTime.Today);
            
            if (!File.Exists(statmentFileName))
            {
                //string statementFileHeader = $"Logged_user\t Transaction\t Account_holder\t Amount\t Balance\t Transaction_time\n";
                string[] statementFileHeader = new string[] { "Logged user", "Transaction", "Account holder", "Amount", "Balance", "Transaction_time" };
                using (StreamWriter statementFile = new StreamWriter(Helper.StatementFileName(username, DateTime.Today), true))
                statementFile.WriteLine(string.Format(format, statementFileHeader));
                string statementFileHeaderUnderline = "-";
                for (int i = 0; i < 95; i++)
                {
                    statementFileHeaderUnderline += statementFileHeaderUnderline;
                }
                using (StreamWriter statementFile = new StreamWriter(Helper.StatementFileName(username, DateTime.Today), true))
                statementFile.WriteLine(statementFileHeaderUnderline);

            }
        }


        static string format = "{0, -12} {1, -12} {2, -15} {3, 13} {4, 13} {5, -30}";

        

        



    }

}
