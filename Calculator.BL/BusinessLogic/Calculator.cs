using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Calculator.BL.BusinessLogic
{
    public class Calculator : ICalculator
    {

        public Calculation Add(int a, int b)
            {
            using (CalculationContext db = new CalculationContext())
            {
                int c = a + b;
                int d = a - b;
                int e = a * b;
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Calculation calc = new Calculation { Expression = "Add", Result = c, CreatedOn = sqlFormattedDate };
                db.Calculation.Add(calc);
                db.SaveChanges();
                return calc;

            }
        }
        public List<Calculation> ListAll()
        {
            CalculationContext db = new CalculationContext();
            return db.Calculation.ToList();
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
