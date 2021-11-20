using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions
{
    public class EmailFormatException : Exception
    {
        public EmailFormatException(string message) : base(message)
        {
        }

        public EmailFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}