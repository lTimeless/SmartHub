using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SmartHub.Application.Common.Models;
using SmartHub.Application.UseCases.AppFolder.AppConfigParser;

namespace SmartHub.Application.UseCases.Init.CheckHome
{
	/// <summary>
	/// Return true if home exists and false if not
	/// </summary>
	public class CheckHomeHandler : IRequestHandler<CheckHomeQuery, Response<bool>>
	{
		private readonly IAppConfigService _appConfigService;

		public CheckHomeHandler(IAppConfigService appConfigService)
		{
			_appConfigService = appConfigService;
		}

		public async Task<Response<bool>> Handle(CheckHomeQuery request, CancellationToken cancellationToken)
		{
			return _appConfigService.GetConfig().IsActive
				? Response.Ok(true)
				: Response.Fail("No home exists.", false);
		}
	}
}