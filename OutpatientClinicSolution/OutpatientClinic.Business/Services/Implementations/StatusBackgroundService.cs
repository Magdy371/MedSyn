using Microsoft.Extensions.Hosting; // Add this namespace
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OutpatientClinic.Business.Services.Implementations
{
    public class StatusBackgroundService : BackgroundService // Make class public
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly ILogger<StatusBackgroundService> _logger;

        public StatusBackgroundService(
            IBackgroundTaskQueue taskQueue,
            ILogger<StatusBackgroundService> logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);
                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing status update");
                }
            }
        }
    }
}