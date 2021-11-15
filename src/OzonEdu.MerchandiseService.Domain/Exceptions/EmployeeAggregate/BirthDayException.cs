using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class BirthDayException : Exception
    {
        public BirthDayException(string message) : base(message)
        {
        }

        public BirthDayException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}