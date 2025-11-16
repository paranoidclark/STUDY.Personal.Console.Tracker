using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class HelperMethods
    {
        public static double Validation(string message)
        {
            string? input = "";
            double output = 0.0;
            Console.Write(message);
            input = Console.ReadLine();

            while (!double.TryParse(input, out output))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                input = Console.ReadLine();
            }

            return output;
        }
    }
}
