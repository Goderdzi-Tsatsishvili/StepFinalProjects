using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMiniProject
{
    internal class Division : OperationManager
    {
        public Division(double a, double b) : base(a, b) { }
        public override double Calculate()
        {
            if (B == 0)
                throw new Exception("Cannot divide by zero");

            return A / B;
        }
    }
}
