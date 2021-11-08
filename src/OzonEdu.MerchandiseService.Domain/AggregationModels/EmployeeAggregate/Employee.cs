using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class Employee : Entity
    {
        public EmployeeFirstName FirstName { get; }
        public EmployeeMiddleName MiddleName { get; }
        public EmployeeLastName LastName { get; }
        public BirthDay BirthDay { get; }
        public HiringDate HiringDate { get; }
        public Email Email { get; }
        public bool IsDismissed { get; private set; }

        private Employee()
        {
        }

        public Employee(EmployeeFirstName firstName,
            EmployeeMiddleName middleName,
            EmployeeLastName lastName,
            BirthDay birthDay,
            HiringDate hiringDate,
            Email email)
        {
            BirthDay = birthDay;
            Email = email;
            MiddleName = middleName;
            LastName = lastName;
            FirstName = firstName;
            HiringDate = hiringDate;
            IsDismissed = false;
        }

        /// <summary>
        /// Уволить сотрудника
        /// </summary>
        public void Dismiss() => IsDismissed = true;

        /// <summary>
        /// Получить стаж сотрудника (в месяцах)
        /// </summary>
        /// <param name="date">Дата от которой необходимо рассчитать стаж сотрудника</param>
        /// <returns>Стаж сотрудника в месяцах</returns>
        /// <exception cref="EmployeeIsDismissedException">Сотрудник уволен</exception>
        public int GetWorkExperienceInMonths(DateTime date)
        {
            if (IsDismissed) throw new EmployeeIsDismissedException("Employee was dismissed");
            return date.Month - HiringDate.Value.Month;
        }
    }
}