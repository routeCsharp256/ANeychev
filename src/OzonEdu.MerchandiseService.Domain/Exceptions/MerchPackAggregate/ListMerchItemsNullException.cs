using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate
{
    public class ListMerchItemsNullException : Exception
    {
        public ListMerchItemsNullException(string message) : base(message)
        {
        }

        public ListMerchItemsNullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}