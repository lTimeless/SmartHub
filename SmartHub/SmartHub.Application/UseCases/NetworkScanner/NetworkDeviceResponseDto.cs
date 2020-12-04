namespace SmartHub.Application.UseCases.NetworkScanner
{
	public record NetworkDeviceResponseDto(string? Name, string? Description, string? Ipv4, string? Ipv6, string? Hostname, string? MacAddress);

}
