using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Interfaces.Database
{
    public interface IUserRepository
    {
        Task<bool> UsersExist();
        Task<User?> FindByNameAsync(string username);
        Task<User?> FindByIdAsync(string username);
        Task<bool> CreateUser(User user, string pw, string roleName);
        Task<bool> UpdateUser(User user);
        Task<IEnumerable<string>> GetUserRoles(User user);
        Task<bool> UserChangeRole(User user, string newRoleName);

    }
}