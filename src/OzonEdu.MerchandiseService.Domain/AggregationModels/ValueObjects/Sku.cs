using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Товарная позиция, единица складского учёта
    /// </summary>
    public sealed class Sku : ValueObject
    {
        public long Value { get; }
        
        public Sku(long sku)
        {
            Value = sku;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}