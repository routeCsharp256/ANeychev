using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public interface IItemTypeRepository : IRepository<Item>
    {
        Task<IEnumerable<Item>> GetAllTypes(CancellationToken token);
    }
}