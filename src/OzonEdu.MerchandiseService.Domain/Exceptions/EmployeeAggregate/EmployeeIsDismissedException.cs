using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeIsDismissedException : Exception
    {
        public EmployeeIsDismissedException(string message) : base(message)
        {
        }

        public EmployeeIsDismissedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}