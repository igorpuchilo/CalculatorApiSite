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
    public sealed class ApiClient : IDisposable
    {
        private readonly bool useQueryAccessToken;
        private static string userAgent;

        public readonly string Endpoint;

        private class HttpClientEx : HttpClient
        {
            public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpContent content,
                CancellationToken cancellationToken)
            {
                return SendAsync(new HttpRequestMessage(new HttpMethod("Get"), requestUri)
                {
                    Content = content
                }, cancellationToken);
            }
        }

        //private HttpClientEx client = new HttpClientEx();
        private HttpClientEx client = new HttpClientEx { Timeout = new TimeSpan(0, 10, 0) };


        public static void Init(string clientName, string clientRuntimeVersion, string osVersion)
        {
            string version = typeof(ApiClient).Assembly.GetName().Version.ToString();
            userAgent = $"CREXi-{clientName}/{version} Test/{clientRuntimeVersion} ({osVersion})";
        }

        public ApiClient(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentException("Invalid endpoint url");
            this.Endpoint = endpoint;
            this.client.DefaultRequestHeaders.Add("User-Agent", "1.0");
            this.client.DefaultRequestHeaders.Add("Is-Test", true.ToString());
        }

        public void AddHeader(string name, string value)
        {
            this.client.DefaultRequestHeaders.Add(name, value);
        }

        public void Dispose()
        {
            if (this.client != null)
            {
                this.client.Dispose();
                this.client = null;
            }
        }

        internal async Task<ApiResponse> MakeApiRequestAsync(string pathAndQuery, ApiRequestMethod method, object parms,
            string serializedParams = null, MediaTypeFormatter formatter = null)
        {
            ApiResponse result = CreateResponse(pathAndQuery, method, parms, serializedParams);
            try
            {
                HttpResponseMessage responseMessage;
                switch (method)
                {
                    case ApiRequestMethod.POST:
                        responseMessage = formatter != null
                            ? await this.client.PostAsync(result.RequestUri, parms, formatter)
                            : await this.client.PostAsync(result.RequestUri, result.GetContent());
                        break;
                    case ApiRequestMethod.PUT:
                        responseMessage = formatter != null
                            ? await this.client.PutAsync(result.RequestUri, parms, formatter)
                            : await this.client.PutAsync(result.RequestUri, result.GetContent());
                        break;
                    //case ApiRequestMethod.PATCH:
                    //    responseMessage = await this.client.PatchAsync(result.RequestUri, result.GetContent(),
                    //        CancellationToken.None);
                    //    break;
                    case ApiRequestMethod.DELETE:
                        responseMessage = await this.client.DeleteAsync(result.RequestUri);
                        break;
                    default:
                        responseMessage = await this.client.GetAsync(result.RequestUri);
                        break;
                }
                await result.SetResponseAsync(responseMessage);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                result.WebError = ex;
            }
            // w.Stop();
            //Console.Out.WriteLine("API request made. Url = {0}, Method = {1}, Time = {2}ms", result.RequestUri,
            //    result.RequestMethod, w.ElapsedMilliseconds);
            // this.lastResponse = result;
            SetCookie(result);
            return result;
        }

        private ApiResponse CreateResponse(string pathAndQuery, ApiRequestMethod method, object parms,
            string serializedParams = null)
        {
            string content = null;
            HttpContent httpContent = null;
            if (serializedParams != null)
            {
                content = serializedParams;
            }
            else if (parms is HttpContent)
            {
                httpContent = (HttpContent)parms;
            }
            else
            {
                content = SerializeData(parms);
            }
            string url = string.Concat(this.Endpoint, pathAndQuery);
            return new ApiResponse(new Uri(url), method, content, httpContent);
        }

        private static string SerializeData(object data)
        {
            if (data == null)
                return null;
            return JsonConvert.SerializeObject(data);
        }

        public IEnumerable<string> GetCookie()
        {
            IEnumerable<string> values;
            this.client.DefaultRequestHeaders.TryGetValues("Cookie", out values);
            return values;
        }

        private void SetCookie(ApiResponse response)
        {
            if (response.Headers == null)
                return;
            IEnumerable<string> cookie;
            if (response.Headers.TryGetValues("Set-Cookie", out cookie))
            {
                this.client.DefaultRequestHeaders.Add("Cookie", cookie);
            }
        }

        private static string AppendQuery(string path, params string[] parms)
        {
            if (parms == null || parms.Length == 0)
                return path;
            var sb = new StringBuilder(path);
            bool isFirst = path.IndexOf('?') == -1;
            for (int i = 0; i < parms.Length; i += 2)
            {
                sb.Append(isFirst ? '?' : '&');
                sb.Append(parms[i]).Append('=');
                string value = parms[i + 1];
                if (!string.IsNullOrEmpty(value))
                    sb.Append(WebUtility.UrlEncode(value));
                isFirst = false;
            }
            return sb.ToString();
        }
    }
}
//namespace CalculatorTests.Client
//{
//    class ApiClient : IDisposable
//    {
//        //private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
//        //{
//        //    NullValueHandling = NullValueHandling.Ignore,
//        //    DateFormatHandling = DateFormatHandling.IsoDateFormat,
//        //    TypeNameHandling = TypeNameHandling.All
//        //};
//        public string Raw { get; internal set; }
//        private HttpClient client = new HttpClient
//        {
//            BaseAddress = new Uri ("http://localhost/Calculator"),
//            Timeout = new TimeSpan(0, 0, 10)

//    };
//        public void Dispose()
//        {
//            if (this.client != null)
//            {
//                this.client.Dispose();
//                this.client = null;
//            }
//        }
//        public async Task<CalculationResultItemModel> Get_Sum(string url)
//        {
//            CalculationResultItemModel result = null;
//            client.DefaultRequestHeaders.Accept.Clear();
//            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//            HttpResponseMessage response = await client.GetAsync(url);
//            if (response.IsSuccessStatusCode)
//            {
//                result = await response.Content.ReadAsAsync<CalculationResultItemModel>();
//            }
//            return result;
//        }

//        //public T Deserialize<T>()
//        //{
//        //    if (string.IsNullOrWhiteSpace(Raw))
//        //        return default(T);
//        //    return JsonConvert.DeserializeObject<T>(Raw, this.serializerSettings);
//        //}
//    }
//}
