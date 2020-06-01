using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Homes.Read
{
	public class HomesReadHandler : IRequestHandler<HomesReadQuery, ServiceResponse<List<HomeReadResponseDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public HomesReadHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ServiceResponse<List<HomeReadResponseDto>>> Handle(HomesReadQuery request, CancellationToken cancellationToken)
		{
			var homes = await _unitOfWork.HomeRepository.GetAllAsync();
			var homeList = _mapper.Map<List<HomeReadResponseDto>>(homes);
			return new ServiceResponse<List<HomeReadResponseDto>>(homeList, true);
		}
	}
}
