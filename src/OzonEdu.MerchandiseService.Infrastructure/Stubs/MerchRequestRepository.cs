using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public sealed class MerchRequestRepository : IMerchRequestRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public async Task<MerchRequest> CreateAsync(MerchRequest itemToCreate,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<MerchRequest>> GetByEmployeeIdAsync(long employeeId,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchRequest> FindByRequestNumber(RequestNumber requestNumber,
            CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}