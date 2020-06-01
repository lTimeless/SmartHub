using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SmartHub.Application.Common.Exceptions
{
	public class RestException : Exception
	{
		public HttpStatusCode Code { get; }
		public object? Errors { get; }

		public RestException()
		{
		}

		public RestException(string message) : base(message)
		{
		}

		public RestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public RestException(HttpStatusCode code, object? errors = null)
		{
			Code = code;
			Errors = errors;
		}
	}
}
