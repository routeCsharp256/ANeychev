using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class BirthDay : ValueObject
    {
        private readonly DateTime _value;
        public DateTime Value => _value;

        public BirthDay(DateTime value)
        {
            if (DateTime.Now < value) throw new ArgumentException(nameof(BirthDay));
            _value = value;
        }
        
        public BirthDay(string value)
        {
            if (DateTime.TryParse(value, out _value)) throw new ArgumentException(nameof(BirthDay));
            if (DateTime.Now < _value) throw new ArgumentException(nameof(BirthDay));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}