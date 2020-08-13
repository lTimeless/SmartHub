using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Update
{
	public class HomeUpdateCommand : IRequest<Response<HomeDto>>
	{
		public string? Name { get; set; }
		public string? Description { get; set; }

		public string? SettingName { get; set; }
		public string? UserName { get; set; }

		public HomeUpdateCommand(string? name, string? description, string? settingName, string? userName)
		{
			Name = name;
			Description = description;
			SettingName = settingName;
			UserName = userName;
		}
	}
}
