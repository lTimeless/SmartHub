using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Read
{
	public class HomesReadHandler : IRequestHandler<HomesReadQuery, ServiceResponse<HomeDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public HomesReadHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<HomeDto>> Handle(HomesReadQuery request, CancellationToken cancellationToken)
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			var homeDto = _mapper.Map<HomeDto>(home);
			return new ServiceResponse<HomeDto>(homeDto, true);
		}
	}
}
