// unset

using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace SmartHub.Domain.Common.Extensions
{
	public static class TupleExtension
	{
		static Tuple<T?, bool> ParseHttpContent<T>(this Tuple<HttpContent?, bool> tuple)
		{
			var (content, success) = tuple;
			var json = content.ReadFromJsonAsync<T>().ConfigureAwait(false).GetAwaiter().GetResult();
			return new(json, success);
		}
	}
}