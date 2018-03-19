using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Diagnostics;
namespace Calculator.BL.BusinessLogic
{

    public class Calculator : ICalculator
    {
        private CalculationContext _context;
        public Calculator(CalculationContext DbContext)
        {
            _context = DbContext;

            Debug.WriteLine(DbContext.GetHashCode());
        }
        public CalculationDTO Add(int a, int b)
        {
            int c = a + b;
            DateTime myDateTime = DateTime.Now;
            string Date = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Calculation calc = new Calculation { Expression = "Add", Result = c, CreatedOn = Date };
            _context.Calculation.Add(calc);

            _context.SaveChanges();
            return _context.Calculation.Select(CalculationDTO.SelectExpression).OrderByDescending(x => x.CreatedOn).FirstOrDefault();
        }

        public CalculationDTO[] ListAll()
        {
            return _context.Calculation.Select(CalculationDTO.SelectExpression).ToArray();
        }
        public void Delete(int Id)
        {
            Calculation DelId = new Calculation { Id = Id };
            _context.Entry(DelId).State = System.Data.Entity.EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}