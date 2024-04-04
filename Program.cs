using System;
using System.Collections.Generic;
using System.Linq;
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


        }
        private static bool validation(string input)
        {
            if(string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            if (input.Length > 20)
            {
                return false;
            }
            return true;
        }

    }
}
