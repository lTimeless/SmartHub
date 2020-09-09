using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Hangfire;
using Serilog;
using SmartHub.Application.Common.Interfaces;
using SmartHub.Domain.Common.Enums;

namespace SmartHub.Infrastructure.Shared.Services.Dispatchers
{
	/// <inheritdoc cref="IHangfireDispatcher"/>
	public class HangfireDispatcher : IHangfireDispatcher
	{
		private readonly IDateTimeService _dateTimeService;
		private readonly ILogger _log = Log.ForContext(typeof(HangfireDispatcher));

		public HangfireDispatcher(IDateTimeService dateTimeService)
		{
			_dateTimeService = dateTimeService;
		}

		private const int Interval = 10;

		public Task<string> AddJob(Expression<Action> action)
		{
			_log.Information($"Add Job {nameof(action.Name)}");
			return Task.FromResult(BackgroundJob.Enqueue(action));
		}

		public Task<string> AddJob(Expression<Func<Task, Task>> action)
		{
			_log.Information($"Add Job {nameof(action.Name)}");
			return Task.FromResult(BackgroundJob.Enqueue(action));
		}

		public Task<string> AddJob(Expression<Action> action, TimeSpan delay)
		{
			_log.Information($"Add Job {nameof(action.Name)}");
			return Task.FromResult(BackgroundJob.Schedule(action, delay));
		}

		public Task AddRecurringJob(Expression<Action> action, DateTimes recurring, int interval = Interval)
		{
			_log.Information($"Add RecurringJob {action.Name}");
			switch (recurring)
			{
				case DateTimes.Minute:
					RecurringJob.AddOrUpdate(action, $"* 0/{interval} * ? * *");
					break;

				case DateTimes.Hour:
					RecurringJob.AddOrUpdate(action, $"* * 0/{interval} ? * *");
					break;

				case DateTimes.Day:
					RecurringJob.AddOrUpdate(action, $"* * * ? * {_dateTimeService.LocalNow.Day}/{interval} *");
					break;

				case DateTimes.Week:
					RecurringJob.AddOrUpdate(action, $"* * * ? * {_dateTimeService.LocalNow.DayOfWeek.ToString().Substring(0, 2)} *");
					break;

				case DateTimes.Month:
					RecurringJob.AddOrUpdate(action, $"* * * ? 1/{interval} * *");
					break;

				case DateTimes.Year:
					RecurringJob.AddOrUpdate(action, $"* * * ? * * {_dateTimeService.LocalNow.Year}/{interval}");
					break;

				case DateTimes.Never:
					RecurringJob.AddOrUpdate(action, Cron.Never);
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(recurring), recurring, null);
			}
			return Task.CompletedTask;
		}

		public Task UpdateRecurringJob(string jobId, Expression<Action> action, DateTimes recurring, int interval = Interval)
		{
			_log.Information($"Update RecurringJob {nameof(action.Name)}");
			switch (recurring)
			{
				case DateTimes.Minute:
					RecurringJob.AddOrUpdate(jobId, action, $"* 0/{interval} * ? * *");
					break;

				case DateTimes.Hour:
					RecurringJob.AddOrUpdate(jobId, action, $"* * 0/{interval} ? * *");
					break;

				case DateTimes.Day:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * {_dateTimeService.LocalNow.Day}/{interval} *");
					break;

				case DateTimes.Week:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * {_dateTimeService.LocalNow.DayOfWeek.ToString().Substring(0, 2)} *");
					break;

				case DateTimes.Month:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? 1/{interval} * *");
					break;

				case DateTimes.Year:
					RecurringJob.AddOrUpdate(jobId, action, $"* * * ? * * {_dateTimeService.LocalNow.Year}/{interval}");
					break;

				case DateTimes.Never:
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
