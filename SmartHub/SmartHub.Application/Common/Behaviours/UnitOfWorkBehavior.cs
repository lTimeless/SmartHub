using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using SmartHub.Application.Common.Interfaces.Database;

namespace SmartHub.Application.Common.Behaviours
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

		public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            await _unitOfWork.SaveAsync();
        }
    }
}