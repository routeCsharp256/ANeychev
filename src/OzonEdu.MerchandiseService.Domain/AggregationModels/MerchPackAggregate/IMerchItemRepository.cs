using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchItemRepository : IRepository<MerchItem>
    {
        Task<MerchItem> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<MerchItem>> FindByIdsAsync(IReadOnlyList<long> ids, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<MerchItem>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}