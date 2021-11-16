using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate
{
    public class NotEnoughItemsException : Exception
    {
        public NotEnoughItemsException(string message) : base(message)
        {
        }

        public NotEnoughItemsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}