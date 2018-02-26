using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
namespace CalculatorTests.HttpClient
{
    public class HttpClient: IHttpHandler
    {
        private HttpClient _client = new HttpClient();

        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }

        public HttpResponseMessage Delete(string url)
        {
            return GetAsync(url).Result;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }
    }
}
