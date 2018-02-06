using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
namespace Calculator.BL.BusinessLogic
{

    public class Calculator : ICalculator
    {
        
        public CalculationDTO Add(int a, int b)
        {
            using (CalculationContext db = new CalculationContext())
            {
                CalculationDTO calcDto = new CalculationDTO();
                int c = a + b;
                DateTime myDateTime = DateTime.Now;
                string Date = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Calculation calc = new Calculation { Expression = "Add", Result = c, CreatedOn = Date };
                db.Calculation.Add(calc);
                db.SaveChanges();
                calcDto.Equals(calc);
                return calcDto;
            }
        }

        public CalculationDTO[] ListAll()
        {
            using (CalculationContext db = new CalculationContext())
            {
                CalculationDTO list = new CalculationDTO();
                return db.Calculation.Select(CalculationDTO.SelectExpression).ToArray();
            }
        }
        public void Delete(int Id)
        {
            using (CalculationContext db = new CalculationContext())
            {
                Calculation DelId = new Calculation { Id = Id };
                db.Entry(DelId).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}