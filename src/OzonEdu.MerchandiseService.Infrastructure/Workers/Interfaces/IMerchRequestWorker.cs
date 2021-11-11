using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces
{
    public interface IMerchRequestWorker
    {
        Task AutoGenerateMerckRequests(CancellationToken cancellationToken = default);
        Task TryGiveOutMerchPacks(CancellationToken cancellationToken = default);
    }
}