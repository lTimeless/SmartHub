using System.Text.Json.Serialization;

namespace SmartHub.Application.UseCases.GeoLocation
{
	// TODO cahnge to record ?!?!
    public class LocationDto
    {
        [JsonPropertyName("ip")]
        public string? Ip { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("country_name")]
        public string? Country { get; set; }

        [JsonPropertyName("postal")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("latitude")]
        public float Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public float Longitude { get; set; }
    }
}