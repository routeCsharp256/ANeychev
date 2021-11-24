using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class MerchItemRepository : IMerchItemRepository
    {
        public async Task<MerchItem> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<MerchItem>> FindByIdsAsync(IReadOnlyList<long> ids, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<MerchItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}