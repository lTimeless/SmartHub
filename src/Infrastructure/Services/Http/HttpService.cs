using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Extensions;
using System.Collections.Generic;
using System.Net.Http.Json;

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

		/// <inheritdoc cref="IHttpService.SendAsync{T}"/>
		public async Task<Tuple<T?, bool>> SendAsync<T>(string ipAddress, Tuple<string, Dictionary<string, string?>> queryTuple) where T: class
		{
			var (path, queryParams) = queryTuple;
			var uri = new UriBuilder
			{
				Host = ipAddress,
				Path = path
			};
			var url = QueryHelpers.AddQueryString(uri.ToStringWithoutTrailingSlash(), queryParams);
			var request = new HttpRequestMessage(HttpMethod.Get, url);

			using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
			var json = await response.Content.ReadFromJsonAsync<T>();
			return new(json, response.IsSuccessStatusCode);
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
