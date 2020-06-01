using MediatR;
using SmartHub.Domain.Common;
using System.Collections.Generic;

namespace SmartHub.Application.UseCases.Entity.Homes.Read
{
	public class HomesReadQuery : IRequest<ServiceResponse<List<HomeReadResponseDto>>>
	{
	}
}
