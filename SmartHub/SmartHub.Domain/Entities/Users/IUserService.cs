using System.Threading.Tasks;

namespace SmartHub.Domain.Entities.Users
{
	public interface IUserService
	{
		Task<bool> UserChangeRole(User user, string roleName);

		Task<bool> CreateUser(User user, string pw, string roleName);
	}
}
