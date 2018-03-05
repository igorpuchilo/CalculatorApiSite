using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CalculatorTests.Client
{
    class ApiClient : IDisposable
    {
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            TypeNameHandling = TypeNameHandling.All
        };
        public string Raw { get; internal set; }
        private class HttpClientEx : HttpClient
        {
            //Sad smile
        }
        private HttpClientEx client = new HttpClientEx
        {
            BaseAddress = new Uri ("http://localhost:49881/"),
            Timeout = new TimeSpan(0, 10, 0)
        };
        public void Dispose()
        {
            if (this.client != null)
            {
                this.client.Dispose();
                this.client = null;
            }
        }
        public HttpResponseMessage Get(string url)
        {
            return GetAsync(url).Result;
        }
        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await client.GetAsync(url);
        }

        public T Deserialize<T>()
        {
            if (string.IsNullOrWhiteSpace(Raw))
                return default(T);
            return JsonConvert.DeserializeObject<T>(Raw, this.serializerSettings);
        }
    }
}
