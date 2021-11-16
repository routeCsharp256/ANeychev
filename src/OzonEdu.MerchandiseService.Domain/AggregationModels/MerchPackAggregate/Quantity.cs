using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class Quantity : ValueObject
    {
        public Quantity(int value)
        {
            if (value < 0) throw new NegativeValueException($"{nameof(value)} value is negative");
            Value = value;
        }

        public int Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}