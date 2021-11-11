using System;
using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    /// <summary>
    /// Набор товаров мерча
    /// </summary>
    public sealed class MerchPack : Entity
    {
        /// <summary>
        /// Тип набора товаров мерча
        /// </summary>
        public MerchPackType Type { get; }

        /// <summary>
        /// Список товаров в наборе
        /// </summary>
        public IReadOnlyCollection<MerchItem> Items => _items;

        private List<MerchItem> _items = new();

        public MerchPack(MerchPackType packType, List<MerchItem> items)
        {
            Type = packType ?? throw new ArgumentNullException(nameof(packType));
            if (items is null) throw new ArgumentNullException(nameof(items));
            if (items.Count == 0) throw new ArgumentException(null, nameof(items));
            _items = items;
        }
    }
}