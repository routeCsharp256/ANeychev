using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate
{
    public class MerchTypeNullException : Exception
    {
        public MerchTypeNullException(string message) : base(message)
        {
        }

        public MerchTypeNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}