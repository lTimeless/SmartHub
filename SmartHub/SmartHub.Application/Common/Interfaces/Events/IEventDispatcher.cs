
using System;
using Microsoft.Extensions.Hosting;

namespace SmartHub.Application.Common.Interfaces.Events
{
	public interface IEventDispatcher : IInitialize, IHostedService, IDisposable
	{
	}
}
