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
        public Calculation Get(int a, int b)
        {
            return _caclulator.Add(a,b);
        }
        public string Get(int Id)
        {
            return ("Delete by Id = "+ Id + "Ok?!");
        }
        public List<Calculation> Get (string All)
        {
            return _caclulator.ListAll();
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
