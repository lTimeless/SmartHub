using MediatR;
using SmartHub.Domain.Common;
using System.Collections.Generic;
using SmartHub.Application.Common.Models;

namespace SmartHub.Application.UseCases.Entity.Homes.Read
{
	public class HomesReadQuery : IRequest<Response<HomeDto>>
	{
	}
}
