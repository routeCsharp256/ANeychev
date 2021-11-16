using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class Item : Entity
    {
        public ItemType Type { get; }

        public Item(ItemType type)
        {
            Type = type;
        }
    }
}