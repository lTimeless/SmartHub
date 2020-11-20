using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.Init.App
{
	public class AppConfigInitCommand : IRequest<Response<AppConfig>>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool AutoDetectAddress { get; set; }
		public AppConfigInitCommand(string name, string description, bool autoDetectAddress)
		{
			Name = name;
			Description = description;
			AutoDetectAddress = autoDetectAddress;
		}
	}
}
