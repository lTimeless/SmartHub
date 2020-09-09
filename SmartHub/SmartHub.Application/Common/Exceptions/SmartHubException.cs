using Serilog;
using System;
using System.Runtime.CompilerServices;

namespace SmartHub.Application.Common.Exceptions
{
	public class SmartHubException : Exception
	{
		protected SmartHubException()
		{
		}

		public SmartHubException(string message) : base(message)
		{
		}

		public SmartHubException(string message, [CallerMemberName] string methodName = "", [CallerLineNumber] int lineNumber = 0)
		{
			Log.ForContext(typeof(SmartHubException)).Warning($"{methodName} threw an error on line {lineNumber}, Message {message}");
		}
	}
}
