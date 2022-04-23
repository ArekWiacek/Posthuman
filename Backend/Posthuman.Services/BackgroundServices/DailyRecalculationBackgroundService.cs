using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Posthuman.Core.Services;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Posthuman.Services.BackgroundServices
{
    /// <summary>
    /// This service runs once a day at specified time. 
    /// Currently it traverses habits and checks if any habit was not completed in time - if so, logic is performed on habit
    /// </summary>
    public sealed class DailyRecalculationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHabitsService habitsService;
        private readonly ILogger<DailyRecalculationBackgroundService> logger;

        // private DateTime TodayExecutionTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 7, 10, 0);
        private DateTime TodayExecutionTime = DateTime.Now.AddSeconds(10);
        private readonly int IntervalBetweenRefreshesInSeconds = 3;

        private int executionCount;

        public DailyRecalculationBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<DailyRecalculationBackgroundService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.executionCount = 0;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (TodayExecutionTime < DateTime.Now)
                RescheduleForTomorrow();

            var secondsToExecution = RemainingSecondsToExecution(TodayExecutionTime);
            Debug.WriteLine($"secondsToExecution: {secondsToExecution}.");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (secondsToExecution <= 0)
                {
                    try
                    {
                        await ExecuteJob(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Catched exception: {ex.Message}.");
                    }
                    finally
                    {
                        executionCount++;
                        RescheduleForTomorrow();
                        secondsToExecution = RemainingSecondsToExecution(TodayExecutionTime);
                    }
                }
                else
                {
                    secondsToExecution = RemainingSecondsToExecution(TodayExecutionTime);
                    Debug.WriteLine($"secondsToExecution: {secondsToExecution}. Delaying {IntervalBetweenRefreshesInSeconds} s.");
                    await Task.Delay(IntervalBetweenRefreshesInSeconds * 1000, stoppingToken);
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Debug.WriteLine($"{nameof(DailyRecalculationBackgroundService)} is stopping.");
            await base.StopAsync(stoppingToken);
        }

        private async Task ExecuteJob(CancellationToken stoppingToken)
        {
            Debug.WriteLine($"Executing job...");

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                Debug.WriteLine($"Start processing habits...");
                IHabitsService habitsService = scope.ServiceProvider.GetRequiredService<IHabitsService>();
                await habitsService.ProcessAllHabitsMissedInstances();
            }
        }

        private static int RemainingSecondsToExecution(int hour, int minute, int second)
        {
            var today = DateTime.Today;
            var executionTime = new DateTime(today.Year, today.Month, today.Day, hour, minute, second);
            var remainingSeconds = (executionTime - DateTime.Now).TotalSeconds;
            return (int)remainingSeconds;
        }

        private int RemainingSecondsToExecution(DateTime executionTime)
        {
            var remainingSeconds = (executionTime - DateTime.Now).TotalSeconds;
            return (int)remainingSeconds;
        }

        private int RemainingSecondsToExecution(int secondsFromNow)
        {
            var executionTime = DateTime.Now.AddSeconds(secondsFromNow);
            return RemainingSecondsToExecution(executionTime);
        }

        private void RescheduleForTomorrow()
        {
            TodayExecutionTime = TodayExecutionTime.AddDays(1);
        }
    }
}
