using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Application.Common.Interfaces
{
	/// <summary>
	/// This Service handles all hangfire jobs
	/// </summary>
	public interface IHangfireDispatcher
	{
		/// <summary>
		/// Adds an action
		/// </summary>
		/// <param name="action"></param>
		/// <returns>jobId</returns>
		Task<string> AddJob(Expression<Action> action);

		/// <summary>
		/// Adds a function with string return
		/// </summary>
		/// <param name="action"></param>
		/// <returns>jobId</returns>
		Task<string> AddJob(Expression<Func<Task, Task>> action);

		/// <summary>
		/// Adds an action with delay
		/// </summary>
		/// <param name="action"></param>
		/// <param name="delay"></param>
		/// <returns>jobId</returns>
		Task<string> AddJob(Expression<Action> action, TimeSpan delay);

		/// <summary>
		///  Adds an action with recurring time
		/// </summary>
		/// <param name="action"></param>
		/// <param name="recurring"></param>
		/// <param name="interval"></param>
		/// <returns>jobId</returns>
		Task AddRecurringJob(Expression<Action> action, DateTimes recurring, int interval);

		/// <summary>
		/// Updates a job
		/// </summary>
		/// <param name="jobId"></param>
		/// <param name="action"></param>
		/// <param name="recurring"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		Task UpdateRecurringJob(string jobId, Expression<Action> action, DateTimes recurring, int interval);

		/// <summary>
		/// Sets a job continuation
		/// </summary>
		/// <param name="continueJobId"></param>
		/// <param name="action"></param>
		/// <returns>jobId</returns>
		Task<string> ContinueJob(string continueJobId, Expression<Action> action);

		/// <summary>
		/// Deletes a job
		/// </summary>
		/// <param name="jobId"></param>
		/// <returns>true or false if the job got deleted</returns>
		Task<bool> DeleteJob(string jobId);
	}
}
