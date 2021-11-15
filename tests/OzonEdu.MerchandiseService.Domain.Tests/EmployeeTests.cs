using System;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void EmployeeWasDismissedDomainEvent_Trigger_Dismiss()
        {
            #region Arrange

            var employee = new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(DateTime.Today),
                Email.Create("test@test.com"));

            #endregion

            #region Act

            employee.Dismiss();

            #endregion

            #region Assert

            Assert.Equal(@"OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate.EmployeeWasDismissedDomainEvent",
                employee.DomainEvents.ToList()[0].ToString());

            #endregion
        }

        [Fact]
        public void EmployeeWasHiredDomainEvent_Trigger_CheckWorkExperience()
        {
            #region Arrange

            var employee = new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com"));

            #endregion

            #region Act

            employee.CheckWorkExperience(new DateTime(2021, 09, 10));

            #endregion

            #region Assert

            Assert.Equal(@"OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate.EmployeeWasHiredDomainEvent",
                employee.DomainEvents.ToList()[0].ToString());

            #endregion
        }

        [Fact]
        public void EmployeeFinishedProbationPeriodDomainEvent_Trigger_CheckWorkExperience()
        {
            #region Arrange

            var employee = new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com"));

            #endregion

            #region Act

            employee.CheckWorkExperience(new DateTime(2022, 01, 11));

            #endregion

            #region Assert

            Assert.Equal(
                @"OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate.EmployeeFinishedProbationPeriodDomainEvent",
                employee.DomainEvents.ToList()[0].ToString());

            #endregion
        }

        [Fact]
        public void EmployeeBecameVeteranDomainEvent_Trigger_CheckWorkExperience()
        {
            #region Arrange

            var employee = new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com"));

            #endregion

            #region Act

            employee.CheckWorkExperience(new DateTime(2026, 10, 11));

            #endregion

            #region Assert

            Assert.Equal(
                @"OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate.EmployeeBecameVeteranDomainEvent",
                employee.DomainEvents.ToList()[0].ToString());

            #endregion
        }

        [Fact]
        public void EmployeeFirstName_Set_Null_Should_Return_EmployeeFirstNameException()
        {
            Assert.Throws<EmployeeFirstNameException>(() => new Employee(new EmployeeFirstName(null),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void EmployeeFirstName_Set_Empty_Value_Should_Return_EmployeeFirstNameException()
        {
            Assert.Throws<EmployeeFirstNameException>(() => new Employee(new EmployeeFirstName(string.Empty),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void EmployeeMiddleName_Set_Null_Should_Return_EmployeeMiddleNameException()
        {
            Assert.Throws<EmployeeMiddleNameException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName(null),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void EmployeeMiddleName_Set_Empty_Value_Should_Return_EmployeeMiddleNameException()
        {
            Assert.Throws<EmployeeMiddleNameException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName(string.Empty),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void EmployeeLastName_Set_Null_Should_Return_EmployeeLastNameException()
        {
            Assert.Throws<EmployeeLastNameException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName(null),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void EmployeeLastName_Set_Empty_Value_Should_Return_EmployeeLastNameException()
        {
            Assert.Throws<EmployeeLastNameException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName(string.Empty),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void BirthDay_Set_Today_Should_Return_BirthDayException()
        {
            Assert.Throws<BirthDayException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(DateTime.Today),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void HiringDate_Set_Tomorrow_Should_Return_HiringDateException()
        {
            Assert.Throws<HiringDateException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(DateTime.Today.AddDays(1)),
                Email.Create("test@test.com")));
        }

        [Fact]
        public void Email_Set_Null_Should_Return_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create(null)));
        }

        [Fact]
        public void Email_Set_Empty_Should_Return_EmailFormatException()
        {
            Assert.Throws<EmailFormatException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create(string.Empty)));
        }

        [Fact]
        public void Email_Set_Invalid_Value_Should_Return_EmailFormatException()
        {
            Assert.Throws<EmailFormatException>(() => new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create("mail#mail/ru")));
        }

        [Fact]
        public void Email_Set_Valid_Value_Should_Return_Normal_Email()
        {
            #region Arrange

            var expectedEmail = @"mail@mail.ru";

            #endregion

            #region Act

            var employee = new Employee(new EmployeeFirstName("test"),
                new EmployeeMiddleName("test"),
                new EmployeeLastName("test"),
                new BirthDay(new DateTime(1977, 12, 12)),
                new HiringDate(new DateTime(2021, 09, 09)),
                Email.Create(expectedEmail));

            #endregion

            #region Assert

            Assert.Equal(expectedEmail, employee.Email.Value);

            #endregion
        }
    }
}