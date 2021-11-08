using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class MerchItem : Entity
    {
        public Sku Sku { get; }
        public Name Name { get; }
        public Item ItemType { get; }
        public Quantity Quantity { get; }

        public MerchItem(Sku sku,
            Name name,
            Item itemType,
            Quantity quantity)
        {
            Sku = sku;
            Name = name;
            ItemType = itemType;
            Quantity = quantity;
        }
    }
}