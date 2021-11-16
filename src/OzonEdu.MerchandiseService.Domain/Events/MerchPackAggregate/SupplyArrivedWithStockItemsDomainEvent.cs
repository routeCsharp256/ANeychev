using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.Events.MerchPackAggregate
{
    /// <summary>
    /// Пришла поставка с новыми товарами
    /// </summary>
    public class SupplyArrivedWithStockItemsDomainEvent : INotification
    {
        public Sku StockItemSku { get; }
        public Quantity Quantity { get; }

        public SupplyArrivedWithStockItemsDomainEvent(Sku stockItemSku,
            Quantity quantity)
        {
            StockItemSku = stockItemSku;
            Quantity = quantity;
        }
    }
}