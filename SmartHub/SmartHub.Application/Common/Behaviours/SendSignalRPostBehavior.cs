using MediatR.Pipeline;
using SmartHub.Application.UseCases.SignalR.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Behaviours
{
	/// <summary>
	/// Sends at the end of the request pipeline objects over SignalR
	/// </summary>
	public class SendSignalRPostBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
	{
		private readonly ISendOverSignalR _sendOverSignalR;
		public SendSignalRPostBehavior(ISendOverSignalR sendOverSignalR)
		{
			_sendOverSignalR = sendOverSignalR;
		}

		public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
		{
			if (request != null && !request.GetType().Name.EndsWith("Query"))
			{
				await _sendOverSignalR.SendHome();
			}
		}
	}
}
