using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.InitCheck.CheckHome
{
    /// <summary>
    /// Return true if home exists and false if not
    /// </summary>
    public class CheckHomeHandler : IRequestHandler<CheckHomeQuery ,Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckHomeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(CheckHomeQuery request, CancellationToken cancellationToken)
        {
            var homeExist = await _unitOfWork.HomeRepository.Exist().ConfigureAwait(false);
            return homeExist
                ? Response.Ok(true)
                : Response.Fail("No home exists.", false);
        }
    }
}