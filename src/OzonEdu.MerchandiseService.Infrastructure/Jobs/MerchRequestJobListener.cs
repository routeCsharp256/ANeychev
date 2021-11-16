using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace OzonEdu.MerchandiseService.Infrastructure.Jobs
{
    public sealed class MerchRequestJobListener : IJobListener
    {
        private readonly ILogger<MerchRequestJobListener> _logger;

        public MerchRequestJobListener(ILogger<MerchRequestJobListener> logger) =>
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogWarning($"Job {context.JobDetail.JobType.ToString()} to be executed");
            return Task.CompletedTask;
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException? jobException,
            CancellationToken cancellationToken = default)
        {
            _logger.LogWarning($"Job {context.JobDetail.JobType.ToString()} finished");
            return Task.CompletedTask;
        }

        public string Name => "Merch Request job listener";
    }
}