using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    /// Заявка на выдачу мерча
    /// </summary>
    public sealed class MerchRequest : Entity, IAggregationRoot
    {
        /// <summary>
        /// Идентификатор заявки на выдачу мерча
        /// </summary>
        public RequestNumber RequestNumber { get; }

        /// <summary>
        /// Статус заявки на выдачу мерча
        /// </summary>
        public RequestStatus Status { get; private set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; private set; }

        /// <summary>
        /// Электронная почта сотрудника
        /// </summary>
        public Email Email { get; private set; }

        /// <summary>
        /// Список мерча
        /// </summary>
        public IReadOnlyCollection<MerchItem> Items => _items;

        private List<MerchItem> _items = new();

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public MerchRequest()
        {
            RequestNumber = new RequestNumber(new Guid());
            Status = RequestStatus.Draft;
        }

        /// <summary>
        /// Конструктор 1
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="email"></param>
        /// <exception cref="Exception"></exception>
        public MerchRequest(long employeeId, Email email)
            : this()
        {
            EmployeeId = employeeId;
            Email = email;
            Status = RequestStatus.Created;
        }

        /// <summary>
        /// Конструктор 3
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="email"></param>
        /// <param name="items"></param>
        public MerchRequest(long employeeId,
            Email email,
            List<MerchItem> items)
            : this(employeeId, email)
        {
            Status = RequestStatus.InProgress;
            _items = items;
        }

        /// <summary>
        /// Создать заявку для конкретного сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="email"></param>
        /// <exception cref="Exception"></exception>
        public void Create(long employeeId, Email email)
        {
            if (!Equals(Status, RequestStatus.Draft))
            {
                throw new Exception("Incorrect request status");
            }

            EmployeeId = employeeId;
            Email = email ?? throw new Exception("Email should not be null");
            Status = RequestStatus.Created;
        }

        /// <summary>
        /// Взять заявку на выдачу мерча в работу
        /// </summary>
        /// <param name="items"></param>
        /// <exception cref="Exception"></exception>
        public void StartWork(List<MerchItem> items)
        {
            if (!Equals(Status, RequestStatus.Created)) throw new Exception("Incorrect request status");

            _items = items;
            Status = RequestStatus.InProgress;
        }

        /// <summary>
        /// Завершить работу по заявке
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Complete()
        {
            if (!Equals(Status, RequestStatus.InProgress)) throw new Exception("Incorrect request status");

            Status = RequestStatus.Done;
        }
    }
}