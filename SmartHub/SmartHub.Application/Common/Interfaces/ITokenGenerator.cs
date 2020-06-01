using SmartHub.Domain.Entities.Users;

namespace SmartHub.Application.Common.Interfaces
{
	public interface ITokenGenerator
	{
		/// <summary>
		/// Generates a random token with a default length of 32chars
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		string GenerateToken(int size = 32);

		/// <summary>
		/// Creates a jwt token based on the given user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		string CreateJwtToken(User user);
	}
}
