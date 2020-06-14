using MediatR;
using SmartHub.Domain.Common;
using System.Threading;
using System.Threading.Tasks;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Identity.Registration
{
	public class RegistrationHandler : IRequestHandler<RegistrationCommand, ServiceResponse<AuthResponseDto>>
	{
		private readonly IRegistrationService _registrationService;

		public RegistrationHandler(IRegistrationService registrationService)
		{
			_registrationService = registrationService;
		}

		public async Task<ServiceResponse<AuthResponseDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
		{
			var result = await _registrationService.RegisterAsync(request).ConfigureAwait(false);
			return new ServiceResponse<AuthResponseDto>(result, true, "Successful");
		}
	}
}
