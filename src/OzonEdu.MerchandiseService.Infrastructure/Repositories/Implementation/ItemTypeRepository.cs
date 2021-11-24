using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Repositories.Implementation
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IQueryExecutor _queryExecutor;
        private const int Timeout = 5;

        public ItemTypeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IQueryExecutor queryExecutor)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _queryExecutor = queryExecutor;
        }

        public async Task<IEnumerable<Item>> GetAllTypes(CancellationToken cancellationToken)
        {

            const string sql = @"
                SELECT item_types.id, item_types.name
                FROM item_types;";
            
            var commandDefinition = new CommandDefinition(
                sql,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            return await _queryExecutor.Execute(async () =>
            {
                var result = await connection.QueryAsync<Models.ItemType>(commandDefinition);
                return result.Select(x => new Item(new ItemType(x.Id, x.Name)));
            });
        }
    }
}