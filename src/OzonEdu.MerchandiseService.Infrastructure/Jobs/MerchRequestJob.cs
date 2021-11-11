using System;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces;
using Quartz;

namespace OzonEdu.MerchandiseService.Infrastructure.Jobs
{
    public sealed class MerchRequestJob : IJob
    {
        private readonly IMerchRequestWorker _merchRequestWorker;

        public MerchRequestJob(IMerchRequestWorker merchRequestWorker) =>
            _merchRequestWorker = merchRequestWorker ?? throw new ArgumentNullException(nameof(merchRequestWorker));


        public async Task Execute(IJobExecutionContext context)
        {
            await _merchRequestWorker.AutoGenerateMerckRequests(context.CancellationToken);
            await _merchRequestWorker.TryGiveOutMerchPacks(context.CancellationToken);
        }
    }
}