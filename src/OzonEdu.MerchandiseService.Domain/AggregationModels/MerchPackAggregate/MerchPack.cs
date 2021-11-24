using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    /// <summary>
    /// Набор товаров мерча
    /// </summary>
    public sealed class MerchPack : Entity, IAggregationRoot
    {
        /// <summary>
        /// Тип набора товаров мерча
        /// </summary>
        public MerchPackType Type { get; }

        /// <summary>
        /// Флаг: Может ли выдаваться повторно?
        /// </summary>
        public bool CanBeReissued { get; }

        /// <summary>
        /// Количество дней с последней выдачи, после которого пак может быть выдан повторно
        /// </summary>
        public CanBeReissuedAfterDays CanBeReissuedAfterDays { get; }

        /// <summary>
        /// Список товаров в наборе
        /// </summary>
        public IReadOnlyCollection<MerchItem> Items => _items;

        private readonly List<MerchItem> _items;

        public MerchPack(MerchPackType packType,
            List<MerchItem> items,
            bool canBeReissued = false,
            CanBeReissuedAfterDays canBeReissuedAfterDays = null)
        {
            Type = packType ?? throw new MerchTypeNullException(nameof(packType));
            if (items is null) throw new ListMerchItemsNullException(nameof(items));
            if (items.Count == 0) throw new ListMerchItemsCountZeroException(nameof(items));
            _items = items;
            CanBeReissued = canBeReissued;
            if (CanBeReissued)
                CanBeReissuedAfterDays = canBeReissuedAfterDays ??
                                         throw new ArgumentNullException(nameof(canBeReissuedAfterDays));
        }
    }
}