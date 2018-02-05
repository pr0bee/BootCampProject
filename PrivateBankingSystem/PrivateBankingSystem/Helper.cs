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
            string password = "";
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

        // Source : https://www.codeproject.com/Articles/769741/Csharp-AES-bits-Encryption-Library-with-Salt .
        private static byte[] AES_Encrypt(byte[] passwordBytes, byte[] passPhraseBytes)
        {
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
            
            byte[] passPhraseBytes = Encoding.UTF8.GetBytes("my_passphrase_for_the_encryption_of_the_password");

            // Hash the password with SHA256
            passPhraseBytes = SHA256.Create().ComputeHash(passPhraseBytes);

            byte[] encryptedPasswordBytes = AES_Encrypt(passwordBytes, passPhraseBytes);

            string cipherPassword = Convert.ToBase64String(encryptedPasswordBytes);

            return cipherPassword;
        }


    }

}
