using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces
{
    public interface IMerchRequestWorker
    {
        Task AutoGenerateMerckRequest(CancellationToken cancellationToken = default);
    }
}