using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository : IRepository<MerchPack>
    {
        Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        
        Task<MerchPack> FindByTypeAsync(MerchPackType type, CancellationToken cancellationToken = default);
    }
}