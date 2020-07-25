using Hangfire;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SmartHub.Application.Common.Utils;
using DateTime = SmartHub.Domain.Common.Enums.DateTime;

namespace SmartHub.Infrastructure.Services.Dispatchers
{
	/// <inheritdoc cref="IHangfireDispatcher"/>
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

		public Task AddRecurringJob(Expression<Action> action, DateTime recurring, int interval = Interval)
		{
			Log.Information($"[{nameof(AddRecurringJob)}] Add RecurringJob {action.Name}");
			switch (recurring)
			{
				case DateTime.Minute:
					RecurringJob.AddOrUpdate(action, $"* 0/{interval} * ? * *");
					break;

				case DateTime.Hour:
					RecurringJob.AddOrUpdate(action, $"* * 0/{interval} ? * *");
					break;

				case DateTime.Day:
					RecurringJob.AddOrUpdate(action, $"* * * ? * {DateTimeUtils.LocalNow.Day}/{interval} *");
					break;

				case DateTime.Week:
					RecurringJob.AddOrUpdate(action, $"* * * ? * {DateTimeUtils.LocalNow.DayOfWeek.ToString().Substring(0, 2)} *");
					break;

				case DateTime.Month:
					RecurringJob.AddOrUpdate(action, $"* * * ? 1/{interval} * *");
					break;

				case DateTime.Year:
					RecurringJob.AddOrUpdate(action, $"* * * ? * * {DateTimeUtils.LocalNow.Year}/{interval}");
					break;

				case DateTime.Never:
					RecurringJob.AddOrUpdate(action, Cron.Never);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(recurring), recurring, null);
			}
			return Task.CompletedTask;
		}

		public Task UpdateRecurringJob(string jobId, Expression<Action> action, DateTime recurring, int interval = Interval)
		{
			Log.Information($"[{nameof(AddRecurringJob)}] Update RecurringJob {nameof(action.Name)}");
			switch (recurring)
			{
				case DateTime.Minute:
					RecurringJob.AddOrUpdate(jobId, action, $"* 0/{interval} * ? * *");
					break;

				case DateTime.Hour:
					RecurringJob.AddOrUpdate(jobId, action, $"* * 0/{interval} ? * *");
					break;

				case DateTime.Day:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * {DateTimeUtils.LocalNow.Day}/{interval} *");
					break;

				case DateTime.Week:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * {DateTimeUtils.LocalNow.DayOfWeek.ToString().Substring(0, 2)} *");
					break;

				case DateTime.Month:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? 1/{interval} * *");
					break;

				case DateTime.Year:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * * {DateTimeUtils.LocalNow.Year}/{interval}");
					break;

				case DateTime.Never:
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
