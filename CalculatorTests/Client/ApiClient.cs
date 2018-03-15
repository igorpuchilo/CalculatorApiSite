using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Calculator.Models;

namespace CalculatorTests.Client
{
    class ApiClient : IDisposable
    {
        //private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        //{
        //    NullValueHandling = NullValueHandling.Ignore,
        //    DateFormatHandling = DateFormatHandling.IsoDateFormat,
        //    TypeNameHandling = TypeNameHandling.All
        //};
        public string Raw { get; internal set; }
        private HttpClient client = new HttpClient
        {
            BaseAddress = new Uri ("http://localhost:49881/"),
            Timeout = new TimeSpan(0, 0, 10)
            
    };
        public void Dispose()
        {
            if (this.client != null)
            {
                this.client.Dispose();
                this.client = null;
            }
        }
        public async Task<CalculationResultItemModel> Get_Sum(string url)
        {
            CalculationResultItemModel result = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<CalculationResultItemModel>();
            }
            return result;
        }

        //public T Deserialize<T>()
        //{
        //    if (string.IsNullOrWhiteSpace(Raw))
        //        return default(T);
        //    return JsonConvert.DeserializeObject<T>(Raw, this.serializerSettings);
        //}
    }
}
