using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Events.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class MerchItem : Entity
    {
        public Sku Sku { get; }
        public Name Name { get; }
        public Item ItemType { get; }
        public Quantity Quantity { get; private set; }

        public MerchItem(Sku sku,
            Name name,
            Item itemType,
            Quantity quantity)
        {
            Sku = sku;
            Name = name;
            ItemType = itemType;
            SetQuantity(quantity);
        }
        
        private void SetQuantity(Quantity value)
        {
            if (value.Value < 0)
                throw new NegativeValueException($"{nameof(value)} value is negative");

            Quantity = value;
        }
        
        public void GiveOutItems(int valueToGiveOut)
        {
            if (valueToGiveOut < 0)
                throw new NegativeValueException($"{nameof(valueToGiveOut)} value is negative");
            if (Quantity.Value < valueToGiveOut)
                throw new NotEnoughItemsException("Not enough items");
            Quantity = new Quantity(this.Quantity.Value - valueToGiveOut);

            if (Quantity.Value != 0) return;
            
            var orderStartedDomainEvent = new ReachedMinimumMerchItemsNumberDomainEvent(Sku);
            AddDomainEvent(orderStartedDomainEvent);
        }
    }
}