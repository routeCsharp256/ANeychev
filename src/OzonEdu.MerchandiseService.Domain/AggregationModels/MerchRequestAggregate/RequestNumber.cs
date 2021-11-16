using System;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class RequestNumber
    {
        public RequestNumber(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
    }
}