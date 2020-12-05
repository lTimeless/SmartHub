using HotChocolate.Execution.Configuration;

namespace SmartHub.Api.Extensions
{
	public static class GraphQlExtension
	{
		public static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
		{
			// TODO hier dann alle Types/Dtos implementiern

			return builder;
		}
	}
}