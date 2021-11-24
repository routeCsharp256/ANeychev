using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<MerchRequest> CreateAsync(MerchRequest itemToCreate, CancellationToken cancellationToken = default);

        Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<MerchRequest>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<MerchRequest>> FindAsync(Func<MerchRequest, bool> predicate,
            CancellationToken cancellationToken = default);
    }
}