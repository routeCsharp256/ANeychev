using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public class HumanName : ValueObject
    {
        public string Value { get; }
        
        public HumanName(string name)
        {
            // Todo добавить проверку на null
            // Todo добавить проверку на пустое значение
            // Todo добавить проверку на наличие цифр
            // Todo добавить проверку на отсутствие спец. символов. Допускается только дефис.
            Value = name;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}