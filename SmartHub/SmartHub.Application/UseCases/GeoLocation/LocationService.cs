using System.Threading.Tasks;
using SmartHub.Application.Common.Interfaces;

namespace SmartHub.Application.UseCases.GeoLocation
{
    public class LocationService : ILocationService
    {
        private readonly IHttpService _httpService;

        public LocationService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<LocationDto?> GetLocation() =>
            await _httpService.GetAsync<LocationDto>("api.ipdata.co", "https", "api-key=test");
    }
}