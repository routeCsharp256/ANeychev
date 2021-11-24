using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class CompletenessErrorException : Exception
    {
        public CompletenessErrorException(string message) : base(message)
        {
        }

        public CompletenessErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}