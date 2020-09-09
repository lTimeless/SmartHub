using System;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IHomeRepository HomeRepository { get; }
		IUserRepository UserRepository { get; }

		Task SaveAsync();

		Task RollbackAsync();
	}
}
