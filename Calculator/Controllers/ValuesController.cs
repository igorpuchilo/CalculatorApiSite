using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http; 
using System.Web.Http;
using Calculator.BL.BusinessLogic;
using System.Linq.Expressions;
using System.Data.Entity;
using Calculator.Factory;
namespace Calculator.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly CalculationFactory _factory;
        private readonly ICalculator _caclulator;
        public ValuesController(ICalculator calculator, CalculationFactory factory)
        {
            _caclulator = calculator;
            _factory = factory;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        public IHttpActionResult Get(int a, int b)
        {
            return Ok(_factory.GetResult(_caclulator.Add(a,b)));
        }
        public IHttpActionResult Get (string All)
        {
            return Ok(_factory.GetResult(_caclulator.ListAll()));
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
        public IHttpActionResult Delete(int Id)
        {
            _caclulator.Delete(Id);
            return Ok("Delete by Id = " + Id);
        }
    }
}
