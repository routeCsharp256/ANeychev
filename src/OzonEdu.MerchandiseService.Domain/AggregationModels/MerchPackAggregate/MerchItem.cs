using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class MerchItem : Entity
    {
        public Item ItemType { get; }
        public Quantity Quantity { get; private set; }

        public MerchItem(Item itemType, Quantity quantity)
        {
            ItemType = itemType;
            SetQuantity(quantity);
        }

        private void SetQuantity(Quantity value)
        {
            if (value.Value < 0)
                throw new NegativeValueException($"{nameof(value)} value is negative");

            Quantity = value;
        }

        public void IncreaseQuantity(int valueToIncrease)
        {
            if (valueToIncrease < 0)
            {
                throw new NegativeValueException($"{nameof(valueToIncrease)} value is negative");
            }

            Quantity = new Quantity(Quantity.Value + valueToIncrease);
        }

        public void GiveOutItems(int valueToGiveOut)
        {
            if (valueToGiveOut < 0)
                throw new NegativeValueException($"{nameof(valueToGiveOut)} value is negative");
            if (Quantity.Value < valueToGiveOut)
                throw new NotEnoughItemsException("Not enough items");
            Quantity = new Quantity(Quantity.Value - valueToGiveOut);
        }
    }
}