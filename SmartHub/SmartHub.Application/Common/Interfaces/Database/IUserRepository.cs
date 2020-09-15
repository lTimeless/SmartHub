using System.Threading.Tasks;
using SmartHub.Domain.Entities;

namespace SmartHub.Application.Common.Interfaces.Database
{
    public interface IUserRepository
    {
        Task<User> GetUserByName(string username);
        Task<bool> CreateUser(User user, string pw, string roleName);
        Task<bool> UpdateUser(User user);
        Task<bool> UserChangeRole(User user, string newRoleName);

    }
}