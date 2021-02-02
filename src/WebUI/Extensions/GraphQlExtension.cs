using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHub.Application.UseCases.Entity.Devices.ExtendObjectType;

namespace SmartHub.WebUI.Extensions
{
	public static class GraphQlExtension
	{
		public static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
		{
			builder.AddType<ExtendDeviceType>();
			return builder;
		}
	}
}