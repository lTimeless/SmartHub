using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Models;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.Entity.Homes.Read
{
	public class HomeGetHandler : IRequestHandler<HomeGetQuery, Response<AppDto>>
	{
		private readonly IOptions<AppConfig> _appConfig;
		private readonly IMapper _mapper;

		public HomeGetHandler(IMapper mapper, IOptions<AppConfig> appConfig)
		{
			_mapper = mapper;
			_appConfig = appConfig;
		}

		public async Task<Response<AppDto>> Handle(HomeGetQuery request, CancellationToken cancellationToken)
		{
			var home = await _unitOfWork.HomeRepository.GetHome();
			return Response.Ok(_mapper.Map<AppDto>(home));
		}
	}
}
