using MediatR;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Create
{
	public class HomeCreateCommand : IRequest<ServiceResponse<HomeDto>>
	{
		public string Name { get; }
		public string Description { get; }

		public HomeCreateCommand(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
