using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SmartHub.Application.Common.Models;
using SmartHub.Domain;

namespace SmartHub.Application.UseCases.Init.CheckHome
{
	/// <summary>
	/// Return true if home exists and false if not
	/// </summary>
	public class CheckHomeHandler : IRequestHandler<CheckHomeQuery, Response<bool>>
	{
		private readonly IOptions<AppConfig> _appConfig;

		public CheckHomeHandler(IOptions<AppConfig> appConfig)
		{
			_appConfig = appConfig;
		}

		public async Task<Response<bool>> Handle(CheckHomeQuery request, CancellationToken cancellationToken)
		{
			return _appConfig.Value.IsActive
				? Response.Ok(true)
				: Response.Fail("No home exists.", false);
		}
	}
}