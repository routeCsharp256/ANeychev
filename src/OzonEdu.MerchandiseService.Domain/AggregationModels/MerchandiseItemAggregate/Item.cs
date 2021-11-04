namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchandiseItemAggregate
{
    public sealed class Item
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }
}