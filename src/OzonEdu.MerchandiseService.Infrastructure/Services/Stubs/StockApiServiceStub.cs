using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Models;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Services.Stubs
{
    public class StockApiServiceStub : IStockApiService
    {
        public async Task<IReadOnlyList<ItemTypeDto>> GetItemTypesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<StockItemQuantityDto>> GetStockItemsAvailabilityAsync(
            IEnumerable<MerchRequestItem> items, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<StockItemDto>> GetAllStockItemsAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> GiveOutItemsAsync(IReadOnlyList<StockItemQuantityDto> items, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<ItemTypeDto>> GetByItemTypeAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}