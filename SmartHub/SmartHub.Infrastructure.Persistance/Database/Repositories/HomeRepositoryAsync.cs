using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Entities;

namespace SmartHub.Infrastructure.Database.Repositories
{
    public class HomeRepositoryAsync : BaseRepositoryAsync<Home> , IHomeRepository
    {
        private readonly DbSet<Home> _entities;

        public HomeRepositoryAsync(AppDbContext appDbContext) : base(appDbContext)
        {
            _entities = appDbContext.Homes;
        }

        public async Task<Home?> GetHome()
        {
            return await _entities.FirstOrDefaultAsync();
        }
    }
}