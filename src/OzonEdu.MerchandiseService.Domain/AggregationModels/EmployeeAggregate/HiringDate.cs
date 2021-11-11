using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class HiringDate : ValueObject
    {
        private readonly DateTime _value;
        public DateTime Value => _value;

        public HiringDate(DateTime value) =>
            _value = DateTime.Now >= value ? value : throw new ArgumentException(nameof(value));

        public HiringDate(string value)
        {
            if (DateTime.TryParse(value, out _value)) throw new ArgumentException();
            if (DateTime.Now <= _value) throw new ArgumentException(nameof(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}