// unset

namespace SmartHub.Domain.Common.Enums
{
	public enum AppErrorCodes
	{
		// General
		ServerError,
		NotFound,
		NotCreated,
		NotUpdated,
		Exists,
		NoHome,
		// DeviceState
		NotSet,
		// Identity
		NotAuthorized,
		// Input
		IsEmpty,
		// Group
		IsSubGroup
		// Device
	}
}