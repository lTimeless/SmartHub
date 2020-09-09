using System.Threading.Tasks;

namespace SmartHub.Application.UseCases.GeoLocation
{
    public interface ILocationService
    {
        Task<LocationDto?> GetLocation();
    }
}