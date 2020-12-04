using System;
using System.Threading.Tasks;

namespace SmartHub.Application.Common.Interfaces.Database
{
	public interface IUnitOfWork : IDisposable
	{
		Task SaveAsync();

		Task RollbackAsync();
	}
}
