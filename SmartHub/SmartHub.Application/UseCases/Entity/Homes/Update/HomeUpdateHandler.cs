using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Application.Common.Models;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.UseCases.Entity.Homes.Update
{
	public class HomeUpdateHandler : IRequestHandler<HomeUpdateCommand, Response<HomeDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public HomeUpdateHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Response<HomeDto>> Handle(HomeUpdateCommand request, CancellationToken cancellationToken)
		{
			var homeEntity = await _unitOfWork.HomeRepository.GetHome();
			User? userEntity = null;
			Setting? settingEntity = null;
			if (request.UserName != null)
			{
				userEntity = await _unitOfWork.UserRepository.GetUserByName(request.UserName);
			}
			if (request.SettingName != null)
			{
				settingEntity = homeEntity.Settings.Find(x => x.Name == request.SettingName);
			}
			homeEntity.UpdateHome(request.Name, request.Description, settingEntity, userEntity);
			await _unitOfWork.HomeRepository.UpdateAsync(homeEntity);
			await _unitOfWork.SaveAsync();
			return Response.Ok("Updated home", _mapper.Map<HomeDto>(homeEntity));

		}
	}
}
