using System;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    /// Заявка на выдачу мерча
    /// </summary>
    public sealed class MerchRequest : Entity, IAggregationRoot
    {
        /// <summary>
        /// Дата создания заявки
        /// </summary>
        public DateTimeOffset DateCreated { get; private set; }

        /// <summary>
        /// Дата завершения заявки
        /// </summary>
        public DateTimeOffset DateOfCompleted { get; private set; }

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
        public MerchPackType MerchPackType { get; private set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; private set; }

        /// <summary>
        /// Размер сотрудника на момент создания заявки
        /// </summary>
        public ClothingSize ClothingSize { get; private set; }

        /// <summary>
        /// Электронная почта сотрудника
        /// </summary>
        public Email EmployeeEmail { get; private set; }

        /// <summary>
        /// Электронная почта менеджера 
        /// </summary>
        public Email ManagerEmail { get; private set; }

        /// <summary>
        /// Список мерча
        /// </summary>
        public IReadOnlyCollection<MerchRequestItem> Items => _items;

        private List<MerchRequestItem> _items = new();

        /// <summary>
        /// Конструктор Empty
        /// </summary>
        public MerchRequest()
        {
            RequestNumber = new RequestNumber(new Guid());
            Status = RequestStatus.Draft;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="clothingSize"></param>
        /// <param name="employeeEmail"></param>
        /// <param name="managerEmail"></param>
        /// <param name="merchPack"></param>
        /// <param name="merchRequestItems"></param>
        /// <param name="dateCreated"></param>
        public MerchRequest(long employeeId,
            ClothingSize clothingSize,
            Email employeeEmail,
            Email managerEmail,
            MerchPack merchPack,
            List<MerchRequestItem> merchRequestItems)
            : this()
        {
            if (merchRequestItems is null) throw new ArgumentNullException(nameof(merchRequestItems));
            if (merchRequestItems.Count == 0) throw new ListMerchItemsCountZeroException(nameof(merchRequestItems));
            EmployeeId = employeeId != 0 ? employeeId : throw new ArgumentException("Employee id must not equal zero");
            ClothingSize = clothingSize ?? throw new ArgumentNullException(nameof(clothingSize));
            EmployeeEmail = employeeEmail ?? throw new ArgumentNullException(nameof(employeeEmail));
            ManagerEmail = managerEmail ?? throw new ArgumentNullException(nameof(managerEmail));
            MerchPackType = merchPack.Type ?? throw new ArgumentNullException(nameof(merchPack));
            DateCreated = DateTimeOffset.UtcNow;
            _items = merchRequestItems;
            Status = RequestStatus.InProgress;
        }

        /// <summary>
        /// Создать заявку для конкретного сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="clothingSize"></param>
        /// <param name="employeeEmail"></param>
        /// <param name="managerEmail"></param>
        /// <exception cref="Exception"></exception>
        public void Create(long employeeId,ClothingSize clothingSize, Email employeeEmail, Email managerEmail)
        {
            if (!Equals(Status, RequestStatus.Draft)) throw new StatusRequestException("Incorrect request status");
            EmployeeId = employeeId != 0 ? employeeId : throw new ArgumentException("Employee id must not equal zero");
            ClothingSize = clothingSize ?? throw new ArgumentNullException(nameof(clothingSize));
            EmployeeEmail = employeeEmail ?? throw new ArgumentNullException(nameof(employeeEmail));
            ManagerEmail = managerEmail ?? throw new ArgumentNullException(nameof(managerEmail));
            DateCreated = DateTimeOffset.UtcNow;
            Status = RequestStatus.Created;
        }

        /// <summary>
        /// Взять заявку на выдачу мерча в работу
        /// </summary>
        /// <param name="merchPack"></param>
        /// <param name="merchRequestItems"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public void StartWork(MerchPack merchPack, List<MerchRequestItem> merchRequestItems)
        {
            if (!Equals(Status, RequestStatus.Created)) throw new StatusRequestException("Incorrect request status");
            if (merchRequestItems is null) throw new ArgumentNullException(nameof(merchRequestItems));
            if (merchRequestItems.Count == 0) throw new ListMerchItemsCountZeroException(nameof(merchRequestItems));
            if (merchPack is null) throw new ArgumentNullException(nameof(merchPack));
            if (IsNotСompletenessItemList(merchPack, merchRequestItems))
                throw new CompletenessErrorException("Сompleteness is broken");
            MerchPackType = merchPack.Type;
            _items = merchRequestItems;
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
        public void Complete()
        {
            if (!Equals(Status, RequestStatus.InProgress)) throw new StatusRequestException("Incorrect request status");

            Status = RequestStatus.Done;
            DateOfCompleted = DateTimeOffset.UtcNow;
            var merchRequestWasDoneDomainEvent = new MerchRequestWasDoneDomainEvent();
            AddDomainEvent(merchRequestWasDoneDomainEvent);
        }

        /// <summary>
        /// Проверка списка мерча на комплектность
        /// </summary>
        /// <returns></returns>
        private static bool IsNotСompletenessItemList(MerchPack merchPack,
            IReadOnlyCollection<MerchRequestItem> merchRequestItems)
        {
            if (merchPack.Items.Count != merchRequestItems.Count) return true;
            foreach (var item in merchPack.Items)
            {
                var checkItem = merchRequestItems.FirstOrDefault(it => it.ItemType == item.ItemType);
                if (checkItem is null) return true;
                if (!Equals(checkItem.Quantity, item.Quantity)) return true;
            }

            return false;
        }
    }
}