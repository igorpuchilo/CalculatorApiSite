using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Calculator.BL.BusinessLogic;
namespace Calculator.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ICalculator _caclulator;
        public ValuesController(ICalculator calculator)
        {
            _caclulator = calculator;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public int Get(int a, int b)
        {
            int c = _caclulator.Add(a, b);
            using (CalculationContext db = new CalculationContext())
            {
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Calculation calc = new Calculation { Expression = "Add", Result = c, CreatedOn = sqlFormattedDate };
                db.Calculation.Add(calc);
                db.SaveChanges();
            }
            return c;
        }
        public string Get(int Id)
        {
            using (CalculationContext db = new CalculationContext())
            {
                Calculation DelId = new Calculation { Id = Id};
                db.Entry(DelId).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return ("Delete by Id = "+ Id + "Ok?!");
        }
        public List<Calculation> Get (string All)
        {

            CalculationContext db = new CalculationContext();
            return db.Calculation.ToList();
        }
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
