using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Models;
using OzonEdu.MerchandiseService.HttpClients.StockApiService.Interfaces;

namespace OzonEdu.MerchandiseService.HttpClients.Stubs
{
    public sealed class StockApiHttpClientStub : IStockApiHttpClient
    {
        private List<StockItem> _stockItems;

        public StockApiHttpClientStub()
        {
            _stockItems = new List<StockItem>
            {
                new(new Sku(1), new Quantity(100), new QuantityValue(10)),
                new(new Sku(2), new Quantity(100), new QuantityValue(10)),
                new(new Sku(3), new Quantity(100), new QuantityValue(10)),
                new(new Sku(4), new Quantity(100), new QuantityValue(10)),
                new(new Sku(5), new Quantity(100), new QuantityValue(10)),
                new(new Sku(6), new Quantity(100), new QuantityValue(10)),
                new(new Sku(7), new Quantity(100), new QuantityValue(10)),
                new(new Sku(8), new Quantity(100), new QuantityValue(10)),
                new(new Sku(9), new Quantity(100), new QuantityValue(10))
            };
        }

        public  Task<int> GetAvailableQuantity(long sku, CancellationToken cancellationToken = default)
        {
            var stockItem = _stockItems.FirstOrDefault(x => x.Sku.Equals(new Sku(sku)));
            if (stockItem != null) throw new ArgumentException($"{nameof(sku)} doesn't exist"); 
            return Task.FromResult(stockItem.Quantity.Value);
        }

        public Task GiveOut(long sku, int quantity, CancellationToken cancellationToken = default)
        {
            var stockItem = _stockItems.FirstOrDefault(x => x.Sku.Equals(new Sku(sku)));
            if (stockItem != null) throw new ArgumentException($"{nameof(sku)} doesn't exist");
            stockItem.GiveOutItems(quantity);
            return Task.CompletedTask;
        }
    }

    class StockItem
    {
        public StockItem(Sku sku,
            Quantity quantity,
            QuantityValue minimalQuantity)
        {
            Sku = sku;
            SetQuantity(quantity);
            SetMinimalQuantity(minimalQuantity);
        }
        
        public Sku Sku { get; } 
        public Quantity Quantity { get; private set; }
        public QuantityValue MinimalQuantity { get; private set; }
        
        public void IncreaseQuantity(int valueToIncrease)
        {
            if (valueToIncrease < 0)
                throw new NegativeValueException($"{nameof(valueToIncrease)} value is negative");
            Quantity = new Quantity(this.Quantity.Value + valueToIncrease);
        }

        public void GiveOutItems(int valueToGiveOut)
        {
            if (valueToGiveOut < 0)
                throw new NegativeValueException($"{nameof(valueToGiveOut)} value is negative");
            if (Quantity.Value < valueToGiveOut)
                throw new NotEnoughItemsException("Not enough items");
            Quantity = new Quantity(this.Quantity.Value - valueToGiveOut);
        }
        
        private void SetQuantity(Quantity value)
        {
            if (value.Value < 0)
                throw new NegativeValueException($"{nameof(value)} value is negative");

            Quantity = value;
        }

        private void SetMinimalQuantity(QuantityValue value)
        {
            if (value is null)
                throw new ArgumentNullException($"{nameof(value)} is null");
            if (value.Value is not null && value.Value < 0)
                throw new NegativeValueException($"{nameof(value)} value is negative");

            MinimalQuantity = value;
        }
    }

    class QuantityValue : ValueObject
    {
        public QuantityValue(int? value)
        {
            Value = value;
        }

        public int? Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}