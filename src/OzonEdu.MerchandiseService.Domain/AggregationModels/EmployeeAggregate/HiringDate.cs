using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class HiringDate : ValueObject
    {
        private readonly DateTime _value;
        public DateTime Value => _value;

        public HiringDate(DateTime value)
        {
            _value = value;
            // Todo добавить проверку на "завтра"
        }

        public HiringDate(string value)
        {
            if (DateTime.TryParse(value, out _value)) throw new ArgumentException();
            // Todo добавить проверку на "завтра"
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}