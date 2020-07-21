using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Interfaces.Repositories
{
    public interface IHomeRepository : IBaseRepository<Home>
    {
        Task<Home> GetHome();
    }
}