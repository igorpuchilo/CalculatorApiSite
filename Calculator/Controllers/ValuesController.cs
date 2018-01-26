using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Calculator.BL;
namespace Calculator.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public IHttpActionResult Get(int? a, int? b)
        {
            if ((a.HasValue) && (b.HasValue))
            {
                //update
                SomeBL objsum = new SomeBL();
                return Ok(objsum.Summ(a.Value, b.Value));
            }
            else
            {
                return BadRequest();
            }
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
