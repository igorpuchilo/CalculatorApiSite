using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Calculator.BL.BusinessLogic
{
    public class Calculator : ICalculator
    {

        public int Add(int a, int b)
            {
            return a+b;
            }
    }
}
