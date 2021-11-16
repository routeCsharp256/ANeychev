using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public sealed class MerchPackRepository : IMerchPackRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public async Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchPack> UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}