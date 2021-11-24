using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
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