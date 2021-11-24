using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public sealed class MerchRequestItem : Entity
    {
        public Sku Sku { get; }
        
        public Name Name { get; }
        
        public Item ItemType { get; }
        
        public Quantity Quantity { get; }

        public MerchRequestItem(Sku sku, Name name, Item itemType, Quantity quantity)
        {
            Sku = sku ?? throw new ArgumentNullException(nameof(sku));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ItemType = itemType ?? throw new ArgumentNullException(nameof(itemType));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
        }
    }
}