using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeFirstNameException : Exception
    {
        public EmployeeFirstNameException(string message) : base(message)
        {
        }

        public EmployeeFirstNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}