using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    /// Заявка на выдачу мерча
    /// </summary>
    public sealed class MerchRequest : Entity, IAggregationRoot
    {
        /// <summary>
        /// Дата завершения заявки
        /// </summary>
        private DateTime _dateOfCompleted;

        /// <summary>
        /// Идентификатор заявки на выдачу мерча
        /// </summary>
        public RequestNumber RequestNumber { get; }

        /// <summary>
        /// Статус заявки на выдачу мерча
        /// </summary>
        public RequestStatus Status { get; private set; }

        /// <summary>
        /// Тип набора товаров мерча
        /// </summary>
        public MerchPackType Type { get; set; }

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
        /// Конструктор 1
        /// </summary>
        public MerchRequest()
        {
            RequestNumber = new RequestNumber(new Guid());
            Status = RequestStatus.Draft;
        }

        /// <summary>
        /// Конструктор 2
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="email"></param>
        /// <param name="type"></param>
        /// <exception cref="Exception"></exception>
        public MerchRequest(long employeeId, 
            Email email, 
            MerchPackType type)
            : this()
        {
            EmployeeId = employeeId != 0 ? employeeId : throw new ArgumentException("Employee id must not equal zero");
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Status = RequestStatus.Created;
        }

        /// <summary>
        /// Конструктор 3
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="email"></param>
        /// <param name="type"></param>
        /// <param name="items"></param>
        public MerchRequest(long employeeId,
            Email email,
            MerchPackType type,
            List<MerchItem> items)
            : this(employeeId, email, type)
        {
            if (items is null) throw new ArgumentNullException(nameof(items));
            if (items.Count == 0) throw new ListMerchItemsCountZeroException(nameof(items));
            _items = items;
            Status = RequestStatus.InProgress;
        }

        /// <summary>
        /// Создать заявку для конкретного сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="email"></param>
        /// <exception cref="Exception"></exception>
        public void Create(long employeeId, Email email)
        {
            if (!Equals(Status, RequestStatus.Draft)) throw new StatusRequestException("Incorrect request status");
            EmployeeId = employeeId != 0 ? employeeId : throw new ArgumentException("Employee id must not equal zero");
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Status = RequestStatus.Created;
        }

        /// <summary>
        /// Взять заявку на выдачу мерча в работу
        /// </summary>
        /// <param name="type"></param>
        /// <param name="items"></param>
        /// <exception cref="Exception"></exception>
        public void StartWork(MerchPackType type, List<MerchItem> items)
        {
            if (!Equals(Status, RequestStatus.Created)) throw new StatusRequestException("Incorrect request status");
            Type = type ?? throw new ArgumentNullException(nameof(type));
            if (items is null) throw new ArgumentNullException(nameof(items));
            if (items.Count == 0) throw new ListMerchItemsCountZeroException(nameof(items));
            _items = items;
            Status = RequestStatus.InProgress;
        }

        /// <summary>
        /// Отменить заявку
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Cancel()
        {
            if (!Equals(Status, RequestStatus.InProgress)) throw new StatusRequestException("Incorrect request status");

            Status = RequestStatus.Canceled;
        }

        /// <summary>
        /// Завершить работу по заявке
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Complete(DateTime date)
        {
            if (!Equals(Status, RequestStatus.InProgress)) throw new StatusRequestException("Incorrect request status");

            Status = RequestStatus.Done;
            _dateOfCompleted = date;
            var merchRequestWasDoneDomainEvent = new MerchRequestWasDoneDomainEvent();
            AddDomainEvent(merchRequestWasDoneDomainEvent);
        }

        /// <summary>
        /// Возвращает срок выдачи заявки
        /// </summary>
        /// <param name="date">Дата относительно которой проверяется срок выдачи заявки</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public TimeSpan IsExpiredDateOfGiveOut(DateTime date)
        {
            if (!Equals(Status, RequestStatus.InProgress)) throw new StatusRequestException("Incorrect request status");
            if (_dateOfCompleted > date) throw new ArgumentException("Incorrect date");
            return _dateOfCompleted - date;
        }
    }
}