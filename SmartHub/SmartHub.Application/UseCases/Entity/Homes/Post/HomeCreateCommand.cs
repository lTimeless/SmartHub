using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Post
{
	public class HomeCreateCommand : IRequest<Response<HomeDto>>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool AutoDetectAddress { get; set; }
		public HomeCreateCommand(string name, string description, bool autoDetectAddress)
		{
			Name = name;
			Description = description;
			AutoDetectAddress = autoDetectAddress;
		}
	}
}
