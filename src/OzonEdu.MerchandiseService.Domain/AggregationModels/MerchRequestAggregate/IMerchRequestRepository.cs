using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<MerchRequest> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        
        Task<MerchRequest> FindByRequestNumber(RequestNumber requestNumber, CancellationToken cancellationToken = default);
    }
}