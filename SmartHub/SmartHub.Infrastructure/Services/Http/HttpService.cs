using System;
using System.Net.Http;
using SmartHub.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace SmartHub.Infrastructure.Services.Http
{
	public class HttpService : IHttpService
	{
		private readonly HttpClient _httpClient;
		public HttpService(IHttpClientFactory clientFactory)
		{
			_httpClient = clientFactory.CreateClient();
		}

		public async Task<bool> SendAsync(string ipAddress, string query)
		{
			var uri = new UriBuilder
			{
				Host = ipAddress,
				Query = query
			};
			//bsp. http://192.168.2.23?turn=On
			var request = new HttpRequestMessage(HttpMethod.Get, uri.ToString());
			using var response = await _httpClient.SendAsync(request,  HttpCompletionOption.ResponseHeadersRead);
			return response.IsSuccessStatusCode;
		}

		public Task GetAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}

		public Task PostAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}

		public Task PutAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}
	}
}
