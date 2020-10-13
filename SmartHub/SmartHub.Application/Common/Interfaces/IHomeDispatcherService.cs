using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
    public interface IHomeDispatcherService
    {
        Task SendHomeOverSignalR();
    }
}