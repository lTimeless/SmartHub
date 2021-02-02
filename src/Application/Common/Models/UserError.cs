
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.Common.Models
{
	public record UserError(string Message, AppErrorCodes Code);
}