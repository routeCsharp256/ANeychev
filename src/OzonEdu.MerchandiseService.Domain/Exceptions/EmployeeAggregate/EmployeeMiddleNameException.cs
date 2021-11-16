using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeMiddleNameException : Exception
    {
        public EmployeeMiddleNameException(string message) : base(message)
        {
        }

        public EmployeeMiddleNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}