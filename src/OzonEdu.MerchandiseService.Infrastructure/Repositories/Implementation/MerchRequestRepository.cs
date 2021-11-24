using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public sealed class MerchRequestRepository : IMerchRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory ?? throw new ArgumentNullException(nameof(dbConnectionFactory));
            _changeTracker = changeTracker ?? throw new ArgumentNullException(nameof(changeTracker));
        }

        public async Task<MerchRequest> CreateAsync(MerchRequest itemToCreate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<MerchRequest> UpdateAsync(MerchRequest itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<MerchRequest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<MerchRequest>> FindAsync(Func<MerchRequest, bool> predicate,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}