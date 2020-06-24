using Hangfire;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Enums;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartHub.Infrastructure.Utils;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	/// <summary>
	/// This class will handles all hangfire Jobs and starts/updates/deletes them
	/// </summary>
	public class HangfireDispatcher : IHangfireDispatcher
	{
		private const int Interval = 10;

		public Task<string> AddJob(Expression<Action> action)
		{
			Log.Information($"[{nameof(AddJob)}] Add Job {nameof(action.Name)}");
			return Task.FromResult(BackgroundJob.Enqueue(action));
		}

		public Task<string> AddJob(Expression<Func<Task, Task>> action)
		{
			Log.Information($"[{nameof(AddJob)}] Add Job {nameof(action.Name)}");
			return Task.FromResult(BackgroundJob.Enqueue(action));
		}

		public Task<string> AddJob(Expression<Action> action, TimeSpan delay)
		{
			Log.Information($"[{nameof(AddJob)}] Add Job {nameof(action.Name)}");
			return Task.FromResult(BackgroundJob.Schedule(action, delay));
		}

		public Task AddRecurringJob(Expression<Action> action, DateTimeEnum recurring, int interval = Interval)
		{
			Log.Information($"[{nameof(AddRecurringJob)}] Add RecurringJob {action.Name}");
			switch (recurring)
			{
				case DateTimeEnum.Minute:
					RecurringJob.AddOrUpdate(action, $"* 0/{interval} * ? * *");
					break;

				case DateTimeEnum.Hour:
					RecurringJob.AddOrUpdate(action, $"* * 0/{interval} ? * *");
					break;

				case DateTimeEnum.Day:
					RecurringJob.AddOrUpdate(action, $"* * * ? * {DateTimeUtils.LocalNow.Day}/{interval} *");
					break;

				case DateTimeEnum.Week:
					RecurringJob.AddOrUpdate(action, $"* * * ? * {DateTimeUtils.LocalNow.DayOfWeek.ToString().Substring(0, 2)} *");
					break;

				case DateTimeEnum.Month:
					RecurringJob.AddOrUpdate(action, $"* * * ? 1/{interval} * *");
					break;

				case DateTimeEnum.Year:
					RecurringJob.AddOrUpdate(action, $"* * * ? * * {DateTimeUtils.LocalNow.Year}/{interval}");
					break;

				case DateTimeEnum.Never:
					RecurringJob.AddOrUpdate(action, Cron.Never);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(recurring), recurring, null);
			}
			return Task.CompletedTask;
		}

		public Task UpdateRecurringJob(string jobId, Expression<Action> action, DateTimeEnum recurring, int interval = Interval)
		{
			Log.Information($"[{nameof(AddRecurringJob)}] Update RecurringJob {nameof(action.Name)}");
			switch (recurring)
			{
				case DateTimeEnum.Minute:
					RecurringJob.AddOrUpdate(jobId, action, $"* 0/{interval} * ? * *");
					break;

				case DateTimeEnum.Hour:
					RecurringJob.AddOrUpdate(jobId, action, $"* * 0/{interval} ? * *");
					break;

				case DateTimeEnum.Day:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * {DateTimeUtils.LocalNow.Day}/{interval} *");
					break;

				case DateTimeEnum.Week:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * {DateTimeUtils.LocalNow.DayOfWeek.ToString().Substring(0, 2)} *");
					break;

				case DateTimeEnum.Month:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? 1/{interval} * *");
					break;

				case DateTimeEnum.Year:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * * {DateTimeUtils.LocalNow.Year}/{interval}");
					break;

				case DateTimeEnum.Never:
					RecurringJob.AddOrUpdate(jobId, action, Cron.Never);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(recurring), recurring, null);
			}
			return Task.CompletedTask;
		}

		public Task<string> ContinueJob(string continueJobId, Expression<Action> action)
		{
			return Task.FromResult(BackgroundJob.ContinueJobWith(continueJobId, action));
		}

		public Task<bool> DeleteJob(string jobId)
		{
			return Task.FromResult(BackgroundJob.Delete(jobId));
		}
	}
}
