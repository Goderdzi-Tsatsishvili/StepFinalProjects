using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMiniProject
{
    internal class Subtraction : OperationManager
    {
        public Subtraction(double a, double b) : base(a, b) { }
        public override double Calculate()
        {
            return A - B;
        }
    }
}
