using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Application.Common.Interfaces.Database;
using SmartHub.Application.UseCases.Entity.Homes;
using SmartHub.Application.UseCases.SignalR;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
    public class HomeDispatcherService : IHomeDispatcherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<HomeHub, IServerHub> _hubContext;

        public HomeDispatcherService(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<HomeHub, IServerHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task SendHomeOverSignalR()
        {
            var home = await _unitOfWork.HomeRepository.GetHome();
            if (home == null)
            {
                return;
            }

            await _hubContext.Clients.All.SendHome(_mapper.Map<HomeDto>(home));
        }
    }
}