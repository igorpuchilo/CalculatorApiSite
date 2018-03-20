using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CalculatorTests.Client
{
	public sealed class ApiResponse
	{
		private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
		{
			NullValueHandling = NullValueHandling.Ignore,
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			TypeNameHandling = TypeNameHandling.All
		};

		public readonly Uri RequestUri;
		public readonly ApiRequestMethod RequestMethod;
		public readonly string RequestContent;
		public readonly HttpContent RequestHttpContent;

		public Exception WebError { get; internal set; }
		public HttpStatusCode Status { get; internal set; }

		public bool Success => (int) Status >= 200 && (int) Status < 300;

		public HttpResponseHeaders Headers { get; internal set; }
		public HttpContentHeaders ContentHeaders { get; internal set; }
		public string Raw { get; internal set; }
		public byte[] RawBytes { get; private set; }

		public T Deserialize<T>()
		{
			if (string.IsNullOrWhiteSpace(Raw) || ContentHeaders == null)
				return default(T);
			MediaTypeHeaderValue contentType = ContentHeaders.ContentType;
			if (contentType == null || contentType.MediaType != "application/json")
				return default(T);

			return JsonConvert.DeserializeObject<T>(Raw, this.serializerSettings);
		}

		public ApiErrorData ErrorData()
		{
			return Success ? null : Deserialize<ApiErrorData>();
		}

		internal ApiResponse(Uri url, ApiRequestMethod method, string content, HttpContent httpContent)
		{
			this.RequestUri = url;
			this.RequestMethod = method;
			this.RequestContent = content;
			this.RequestHttpContent = httpContent;
		}

		internal async Task SetResponseAsync(HttpResponseMessage responseMessage)
		{
			Status = responseMessage.StatusCode;
			Headers = responseMessage.Headers;
			if (responseMessage.Content != null)
			{
				ContentHeaders = responseMessage.Content.Headers;
				Raw = await responseMessage.Content.ReadAsStringAsync();
				RawBytes = await responseMessage.Content.ReadAsByteArrayAsync();
			}
		}
	}
}