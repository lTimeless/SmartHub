using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SmartHub.Application.Common.Exceptions;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common;
using SmartHub.Domain.Entities.Homes;
using SmartHub.Domain.Entities.Settings;
using SmartHub.Domain.Entities.Users;
using SmartHub.Domain.Enums;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateHandler : IRequestHandler<HomeCreateCommand, ServiceResponse<HomeCreateResponseDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly Random _random;
		private readonly IConfiguration _configuration;
		private readonly UserManager<User> _userManager;
		private readonly IUserAccessor _userAccessor;

		public HomeCreateHandler(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<User> userManager, IUserAccessor userAccessor
			)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_configuration = configuration;
			_userManager = userManager;
			_userAccessor = userAccessor;
			_random = new Random();
		}

		public async Task<ServiceResponse<HomeCreateResponseDto>> Handle(HomeCreateCommand request, CancellationToken cancellationToken)
		{
			var appSettingsListValues = _configuration.GetSection("AppSettings")
					.GetChildren()
					.ToDictionary(x => x.Key, x => x.GetChildren().Append(x).ToList());
			var solutionRootPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), "Plugins")))
								   ?? throw new SmartHubException("Could not determine solution root path");
			var pluginPath = Path.Combine(solutionRootPath, "Plugins");
			var absolutePath = Path.Combine(solutionRootPath, "Plugins");
			var userName = _userAccessor.GetCurrentUsername();
			User user;
			if (!string.IsNullOrEmpty(userName))
			{
				user = await _userManager.FindByNameAsync(userName);
			}
			else
			{
				throw new RestException(HttpStatusCode.BadRequest, $"Could not find a user -- {userName}");
			}

			var defaultSetting = new Setting($"default_Setting_{_random.Next()}", "this is a default setting", true, absolutePath, pluginPath, pluginPath, userName, SettingTypeEnum.Default);

			var homeEntity = new Home(request.Name, request.Description, defaultSetting);
			homeEntity.AddUser(user);

			var result = await _unitOfWork.HomeRepository.AddAsync(homeEntity);
			if (!result)
			{
				return new ServiceResponse<HomeCreateResponseDto>(null, false, $"[{nameof(Handle)}] Could not create Home");
			}
			await _unitOfWork.SaveAsync().ConfigureAwait(false);

			var homeResponseDto = _mapper.Map<HomeCreateResponseDto>(homeEntity);
			return new ServiceResponse<HomeCreateResponseDto>(homeResponseDto, true);
		}
	}
}
