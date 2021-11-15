using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class BirthDay : ValueObject
    {
        public DateTime Value { get; }

        public BirthDay(DateTime value)
        {
            if (DateTime.Today <= value) throw new BirthDayException(nameof(BirthDay));
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}