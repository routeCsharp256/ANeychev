using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public sealed class MerchItemTests
    {
        [Fact]
        public void SetQuantity_Set_10_Should_Return_10()
        {
            const int expectedQuantity = 10;
            var merchItem = new MerchItem(new Item(ItemType.Bag), new Quantity(expectedQuantity));
            Assert.Equal(expectedQuantity, merchItem.Quantity.Value);
        }

        [Fact]
        public void SetQuantity_Set_Minus_10_Should_Return_NegativeValueException()
        {
            Assert.Throws<NegativeValueException>(() =>
                new MerchItem(new Item(ItemType.Bag), new Quantity(-10)));
        }

        [Fact]
        public void IncreaseQuantity_Set_10_Should_Return_20()
        {
            var merchItem = new MerchItem(new Item(ItemType.Bag), new Quantity(10));
            merchItem.IncreaseQuantity(10);
            Assert.Equal(20, merchItem.Quantity.Value);
        }

        [Fact]
        public void IncreaseQuantity_Set_Minus_10_Should_Return_NegativeValueException()
        {
            var merchItem = new MerchItem(new Item(ItemType.Bag), new Quantity(10));
            Assert.Throws<NegativeValueException>(() => merchItem.IncreaseQuantity(-10));
        }

        [Fact]
        public void GiveOutItems_Set_10_Should_Return_Zero()
        {
            var merchItem = new MerchItem(new Item(ItemType.Bag), new Quantity(10));
            merchItem.GiveOutItems(10);
            Assert.Equal(0, merchItem.Quantity.Value);
        }

        [Fact]
        public void GiveOutItems_Set_Minus_10_Should_Return_NegativeValueException()
        {
            var merchItem = new MerchItem(new Item(ItemType.Bag),
                new Quantity(10));
            Assert.Throws<NegativeValueException>(() => merchItem.GiveOutItems(-10));
        }

        [Fact]
        public void GiveOutItems_Set_20_Should_Return_NotEnoughItemsException()
        {
            var merchItem = new MerchItem(new Item(ItemType.Bag), new Quantity(10));
            Assert.Throws<NotEnoughItemsException>(() => merchItem.GiveOutItems(20));
        }
    }
}