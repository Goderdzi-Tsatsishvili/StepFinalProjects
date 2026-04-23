using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMiniProject
{
    internal class OperationFactory
    {
        public static OperationManager Create(string operation, double a, double b)
        {
            return operation switch
            {
                "+" => new Addition(a, b),
                "-" => new Subtraction(a, b),
                "*" => new Multiplication(a, b),
                "/" => new Division(a, b),
                _ => null
            };
        }
    }
}
