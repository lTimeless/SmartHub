using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.SignalR.Services;

namespace SmartHub.Application.Common.Behaviours
{
    public class UnitOfWorkBehaviour<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendOverSignalR _sendOverSignalR;

		public UnitOfWorkBehaviour(IUnitOfWork unitOfWork, ISendOverSignalR sendOverSignalR)
        {
            _unitOfWork = unitOfWork;
            _sendOverSignalR = sendOverSignalR;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            await _unitOfWork.SaveAsync();
            // If the Request is a command than it probably updated the home Entity
            // so every client needs to be updated with the new State

            // if (request.GetType().Name.EndsWith("Command"))
            // {
                await _sendOverSignalR.SendHome();
            // }
        }
    }
}