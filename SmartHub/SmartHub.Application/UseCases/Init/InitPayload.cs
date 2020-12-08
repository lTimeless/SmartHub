using SmartHub.Application.Common.Models;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.Init
{
	public class InitPayload : Payload
	{
		public AppConfig? AppConfig { get; }

		public InitPayload(AppConfig? appConfig = null, string? message = null) : base(message)
		{
			AppConfig = appConfig;
		}

		public InitPayload(UserError error) : base(new []{ error })
		{
		}
	}
}