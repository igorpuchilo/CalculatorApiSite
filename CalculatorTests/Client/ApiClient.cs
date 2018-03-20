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

        internal async Task<ApiResponse> MakeApiRequestAsync(string pathAndQuery, ApiRequestMethod method)
        {
            ApiResponse result = CreateResponse(pathAndQuery, method);
            try
            {
                HttpResponseMessage responseMessage;
                switch (method)
                {
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
            return result;
        }

        private ApiResponse CreateResponse(string pathAndQuery, ApiRequestMethod method)
        {
            string url = string.Concat(this.Endpoint, pathAndQuery);
            return new ApiResponse(new Uri(url), method, null, null);
        }
    }
}