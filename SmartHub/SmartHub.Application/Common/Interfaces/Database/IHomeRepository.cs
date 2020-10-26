using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Interfaces.Database
{
    public interface IHomeRepository : IBaseRepository<Home>
    {
        Task<Home?> GetHome();
        Task<bool> Exist();
    }
}