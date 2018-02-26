using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorTests.HttpClient
{
   public class Connection
    {
        private IHttpHandler _httpClient;

        public Connection(IHttpHandler httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
