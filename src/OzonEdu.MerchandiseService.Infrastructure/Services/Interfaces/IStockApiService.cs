using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Models;

namespace OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces
{
    public interface IStockApiService
    {
        Task<IReadOnlyList<ItemTypeDto>> GetItemTypesAsync(CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockItemQuantityDto>> GetStockItemsAvailabilityAsync(IEnumerable<MerchRequestItem> items,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<StockItemDto>> GetAllStockItemsAsync(CancellationToken cancellationToken = default);

        Task<bool> GiveOutItemsAsync(IReadOnlyList<StockItemQuantityDto> items,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<ItemTypeDto>> GetByItemTypeAsync(int id, CancellationToken cancellationToken = default);
    }
}