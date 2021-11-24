using System;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public sealed class MerchPackRepository : IMerchPackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchPackRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
            _changeTracker = changeTracker ?? throw new ArgumentNullException(nameof(changeTracker));
        }

        public async Task<MerchPack> CreateAsync(MerchPack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<MerchPack> UpdateAsync(MerchPack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<MerchPack> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<MerchPack> FindByTypeAsync(MerchPackType type, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}