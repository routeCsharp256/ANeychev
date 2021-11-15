using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class HiringDateException : Exception
    {
        public HiringDateException(string message) : base(message)
        {
        }

        public HiringDateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}