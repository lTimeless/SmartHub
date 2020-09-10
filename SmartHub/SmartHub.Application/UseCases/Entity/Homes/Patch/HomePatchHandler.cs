using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Homes.Patch
{
	public class HomePatchHandler : IRequestHandler<HomePatchCommand, Response<HomeDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public HomePatchHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<HomeDto>> Handle(HomePatchCommand request, CancellationToken cancellationToken)
		{
			var homeEntity = await _unitOfWork.HomeRepository.GetHome();
			request.HomePatch.ApplyTo(_mapper.Map<HomeDto>(homeEntity));
			return Response.Ok("Home updated.", _mapper.Map<HomeDto>(homeEntity));

		}
	}
}
