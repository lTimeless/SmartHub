namespace SmartHub.Application.UseCases.NetworkScanner
{
	public class NetworkDeviceResponseDto
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Ipv4 { get; set; }
		public string Ipv6 { get; set; }
		public string Hostname { get; set; }
		public string? MacAddress { get; set; }
	}
}
