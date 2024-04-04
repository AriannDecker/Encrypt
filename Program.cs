using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encrypt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What do you want to encrypt?");

            var input = Console.ReadLine();

            if (!validation(input))
            {
                Console.WriteLine("Input is invalid.");
                return;
            }
            string cipher = Encrypt(input);
            writeFile(cipher);
            string contents = readFile();
            Console.WriteLine(contents);
            string originalTxt = decryption(contents);
            Console.WriteLine(originalTxt);

            Console.ReadKey();
        }
        private static bool validation(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            if (input.Length > 20)
            {
                return false;
            }
            return true;
        }

        private static string Encrypt(string input)
        {
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = secrets.key;
                aesAlg.IV = secrets.iv;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(input);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);
        }
        private static string decryption(string input)
        {
            string plaintext = null;
            byte[] cipherText = Convert.FromBase64String(input);

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = secrets.key;
                aesAlg.IV = secrets.iv;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
        private static void writeFile(string input)
        {
            using(var sw=new StreamWriter("C:\\Users\\Adeck\\OneDrive\\Desktop\\Penn College\\Spring 24\\Encrypt\\data.txt"))
            {
                sw.Write(input);
            }
        }
        private static string readFile()
        {
            using(var sr = new StreamReader("C:\\Users\\Adeck\\OneDrive\\Desktop\\Penn College\\Spring 24\\Encrypt\\data.txt"))
            {
                return sr.ReadLine();
            }
        }
    }

}
