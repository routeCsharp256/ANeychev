using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class Employee : Entity, IAggregationRoot
    {
        public EmployeePersonnelNumber EmployeeId { get; }
        public HumanName FirstName { get; }
        public HumanName MiddleName { get; }
        public HumanName LastName { get; }
        public BirthDay BirthDay { get; }
        public HiringDate HiringDate { get; }
        public Email Email { get; }

        private Employee()
        {
        }

        public Employee(EmployeePersonnelNumber employeeId,
            HumanName firstName,
            HumanName middleName,
            HumanName lastName,
            BirthDay birthDay,
            HiringDate hiringDate,
            Email email)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            MiddleName = middleName;
            BirthDay = birthDay;
            Email = email;
            HiringDate = hiringDate;
            LastName = lastName;
        }
    }
}