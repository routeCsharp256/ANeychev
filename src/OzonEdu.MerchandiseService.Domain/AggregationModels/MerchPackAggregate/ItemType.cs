using System;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class ItemType : Enumeration
    {
        /// <summary>
        /// Футболка
        /// </summary>
        public static ItemType TShirt = new(1, nameof(TShirt));
        
        /// <summary>
        /// Толстовка
        /// </summary>
        public static ItemType Sweatshirt = new(2, nameof(Sweatshirt));
        
        /// <summary>
        /// Блокнот
        /// </summary>
        public static ItemType Notepad = new(3, nameof(Notepad));
        
        /// <summary>
        /// Сумка
        /// </summary>
        public static ItemType Bag = new(4, nameof(Bag));
        
        /// <summary>
        /// Ручка
        /// </summary>
        public static ItemType Pen = new(5, nameof(Pen));
        
        /// <summary>
        /// Носки
        /// </summary>
        public static ItemType Socks = new(6, nameof(Socks));

        public ItemType(int id, string name) : base(id, name)
        {
        }

        public static ItemType Parse(int id) => id switch
        {
            1 => TShirt,
            2 => Sweatshirt,
            3 => Notepad,
            4 => Bag,
            5 => Pen,
            6 => Socks,
            _ => throw new Exception("Unknown type")
        };
    }
}