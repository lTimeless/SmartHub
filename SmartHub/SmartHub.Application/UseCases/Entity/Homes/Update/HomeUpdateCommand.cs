using System.Text.Json.Serialization;
using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Update
{
	public class HomeUpdateCommand : IRequest<Response<HomeDto>>
	{
		public string? Name { get; }
		public string? Description { get; }

		public string? SettingName { get; }
		public string? UserName { get; }

		public HomeUpdateCommand(string? name, string? description, string? settingName, string? userName)
		{
			Name = name;
			Description = description;
			SettingName = settingName;
			UserName = userName;
		}
	}
}
