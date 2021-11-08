using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public class MerchRequestRepository : IMerchRequestRepository
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

        public async Task<MerchRequest> FindByIdAsync(int id, CancellationToken cancellationToken = default)
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