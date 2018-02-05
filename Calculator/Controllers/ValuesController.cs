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
        public IHttpActionResult Get(int a, int b)
        {
            return Ok(_caclulator.Add(a,b));
        }
        public IHttpActionResult Get (string All)
        {
            var ResultArray = _caclulator.ListAll();
            return Ok(ResultArray);
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
