using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeLastNameException : Exception
    {
        public EmployeeLastNameException(string message) : base(message)
        {
        }

        public EmployeeLastNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}