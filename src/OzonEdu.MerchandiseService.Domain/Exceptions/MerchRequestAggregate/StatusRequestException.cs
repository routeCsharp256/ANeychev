using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class StatusRequestException : Exception
    {
        public StatusRequestException(string message) : base(message)
        {
        }

        public StatusRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}