using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public interface IMerchRequestRepository : IRepository<MerchRequest>
    {
        Task<IEnumerable<MerchRequest>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<MerchRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<IEnumerable<MerchRequest>> GetByEmployeeIdAsync(long employeeId, CancellationToken cancellationToken = default);

        Task<MerchRequest> FindByRequestNumber(RequestNumber requestNumber,
            CancellationToken cancellationToken = default);
    }
}