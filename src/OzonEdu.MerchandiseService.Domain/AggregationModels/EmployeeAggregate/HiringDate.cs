using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class HiringDate : ValueObject
    {
        private readonly DateTime _value;
        public DateTime Value => _value;

        public HiringDate(DateTime value) =>
            _value = DateTime.Now >= value ? value : throw new HiringDateException(nameof(value));

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _value;
        }
    }
}