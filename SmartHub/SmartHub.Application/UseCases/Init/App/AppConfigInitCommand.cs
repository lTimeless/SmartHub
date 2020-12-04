using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.Init.App
{
	public record AppConfigInitCommand : IRequest<Response<AppConfig>>
	{
		public string Name { get; init; }
		public string Description { get; init; }
		public bool AutoDetectAddress { get; init; }
	}
}
