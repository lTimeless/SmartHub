using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Infrastructure.Services.Http
{
	/// <inheritdoc cref="IHttpService"/>
	public class HttpService : IHttpService
	{
		private readonly HttpClient _httpClient;
		public HttpService(IHttpClientFactory clientFactory)
		{
			_httpClient = clientFactory.CreateClient("SmartHub");
		}

		/// <inheritdoc cref="IHttpService.SendAsync"/>
		public async Task<bool> SendAsync(string ipAddress, string query)
		{
			var uri = new UriBuilder
			{
				Host = ipAddress,
				Query = query
			};
			//bsp. http://192.168.2.23?turn=On
			var request = new HttpRequestMessage(HttpMethod.Get, uri.ToString());

			using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
			return response.IsSuccessStatusCode;
		}

		/// <inheritdoc cref="IHttpService.GetAsync{T}"/>
		public async Task<T> GetAsync<T>(string ipAddress, string? scheme = "http", string? query = null)
		{
			var uri = new UriBuilder
			{
				Scheme = scheme,
				Host = ipAddress,
				Query = query
			};

			using var response = await _httpClient.GetAsync(uri.ToString());
			var responseAsString = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(responseAsString)!;
		}

		/// <inheritdoc cref="IHttpService.PostAsync"/>
		public Task PostAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}

		/// <inheritdoc cref="IHttpService.PutAsync"/>
		public Task PutAsync(string ipAddress, string query)
		{
			throw new System.NotImplementedException();
		}
	}
}
