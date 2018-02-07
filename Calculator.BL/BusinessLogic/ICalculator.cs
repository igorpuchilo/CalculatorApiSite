using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BL.BusinessLogic
{
    public interface ICalculator
    {
        CalculationDTO Add(int num1, int num2);
        CalculationDTO[] ListAll();
        void Delete(int Id);
    }
}
