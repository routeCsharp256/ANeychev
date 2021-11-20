using System.Collections.Generic;
using System.Text.RegularExpressions;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects
{
    /// <summary>
    /// Электронная почта
    /// </summary>
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string emailString) 
            => Value = emailString;

        public static Email Create(string emailString)
        {
            if (IsValidEmail(emailString)) return new Email(emailString);

            throw new EmailFormatException($"Email is invalid: {emailString}");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static bool IsValidEmail(string emailString)
            => Regex.IsMatch(emailString, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
    }
}