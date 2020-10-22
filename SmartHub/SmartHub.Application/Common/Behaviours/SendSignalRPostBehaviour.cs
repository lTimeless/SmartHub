using MediatR.Pipeline;
using SmartHub.Application.UseCases.SignalR.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Behaviours
{
	/// <summary>
	/// Sends at the end of the request pipeline objects over SignalR
	/// </summary>
	public class SendSignalRPostBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : notnull
	{
		private readonly ISendOverSignalR _sendOverSignalR;
		public SendSignalRPostBehaviour(ISendOverSignalR sendOverSignalR)
		{
			_sendOverSignalR = sendOverSignalR;
		}

		public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
		{
			// If the Request is a command than it probably updated the home Entity
			// so every client needs to be updated with the new State
			if (request.GetType().Name.EndsWith("Command"))
			{
				await _sendOverSignalR.SendHome();
			}
		}
	}
}
