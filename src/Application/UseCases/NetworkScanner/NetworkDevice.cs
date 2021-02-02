namespace SmartHub.Application.UseCases.NetworkScanner
{
	public record NetworkDevice(string? Name, string? Description, string? Ipv4, string? Ipv6, string? Hostname, string? MacAddress);

}
