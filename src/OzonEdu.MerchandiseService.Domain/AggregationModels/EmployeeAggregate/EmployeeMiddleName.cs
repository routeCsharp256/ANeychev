using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class EmployeeMiddleName : ValueObject
    {
        public string Value { get; }
        
        public EmployeeMiddleName(string value)
        {
            Value = !string.IsNullOrEmpty(value) ? value : throw new ArgumentException(nameof(EmployeeFirstName));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}