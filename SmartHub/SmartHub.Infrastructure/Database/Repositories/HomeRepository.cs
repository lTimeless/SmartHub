﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHub.Application.Common.Interfaces.Repositories;
using SmartHub.Domain.Entities.Homes;

namespace SmartHub.Infrastructure.Database.Repositories
{
    public class HomeRepository : BaseRepository<Home> , IHomeRepository
    {
        private readonly DbSet<Home> _entities;

        public HomeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _entities = appDbContext.Homes;
        }

        public async Task<Home> GetHome()
        {
            return await _entities.FirstOrDefaultAsync();
        }
    }
}