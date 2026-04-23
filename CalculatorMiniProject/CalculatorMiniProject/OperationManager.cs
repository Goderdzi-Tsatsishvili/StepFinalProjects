using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorMiniProject
{
    abstract class OperationManager
    {
        protected double A {  get; set; }
        protected double B { get; set; }

        public OperationManager(double a, double b)
        {
            A = a;
            B = b;
        }

        public abstract double Calculate();
    }
}
