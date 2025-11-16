using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculator
    {
        public static double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;

            return result = op switch
            {
                "a" => num1 + num2,
                "s" => num1 - num2,
                "m" => num1 * num2,
                "d" => num2 != 0 ? num1 / num2 : 0,
            };
        }
    }
}
