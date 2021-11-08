using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class EmployeeFirstName : ValueObject
    {
        public string Value { get; }
        
        public EmployeeFirstName(string value)
        {
            Value = !string.IsNullOrEmpty(value) ? value : throw new ArgumentException(nameof(EmployeeFirstName));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}