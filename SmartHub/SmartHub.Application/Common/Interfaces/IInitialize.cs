using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces
{
    public interface IInitialize
    {
        Task Init() => Task.CompletedTask;
    }
}