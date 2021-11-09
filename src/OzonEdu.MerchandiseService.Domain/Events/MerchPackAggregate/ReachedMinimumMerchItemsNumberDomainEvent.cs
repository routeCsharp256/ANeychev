using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;

namespace OzonEdu.MerchandiseService.Domain.Events.MerchPackAggregate
{
    public class ReachedMinimumMerchItemsNumberDomainEvent : INotification
    {
        public Sku StockItemSku { get; }

        public ReachedMinimumMerchItemsNumberDomainEvent(Sku stockItemSku)
        {
            StockItemSku = stockItemSku;
        }
    }
}