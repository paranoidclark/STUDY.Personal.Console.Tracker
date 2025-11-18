using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLPersonDB
{
    public class Helper
    {
        public static string AskUser(string message)
        {

            Console.Write(message);
            string? output = Console.ReadLine().ToLower();

            return output;
        }

        public static int AskUserForInt(string message)
        {
            int output = 0;
            string? input = "";

            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            } while (int.TryParse(input, out output) == false);

            return output;
        }
    }
}
