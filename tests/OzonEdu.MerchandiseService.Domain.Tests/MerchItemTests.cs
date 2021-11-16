using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchItemTests
    {
        [Fact]
        public void SetQuantity_Set_10_Should_Return_10()
        {
            #region Arrange

            const int expectedQuantity = 10;

            #endregion

            #region Act

            var merchItem = new MerchItem(new Sku(1), new Name("test"), new Item(ItemType.Bag),
                new Quantity(expectedQuantity));

            #endregion

            #region Assert

            Assert.Equal(expectedQuantity, merchItem.Quantity.Value);

            #endregion
        }

        [Fact]
        public void SetQuantity_Set_Minus_10_Should_Return_NegativeValueException()
        {
            Assert.Throws<NegativeValueException>(() =>
                new MerchItem(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(-10)));
        }

        [Fact]
        public void IncreaseQuantity_Set_10_Should_Return_20()
        {
            #region Arrange

            var merchItem = new MerchItem(new Sku(1), new Name("test"), new Item(ItemType.Bag),
                new Quantity(10));

            #endregion

            #region Act

            merchItem.IncreaseQuantity(10);

            #endregion

            #region Assert

            Assert.Equal(20, merchItem.Quantity.Value);

            #endregion
        }

        [Fact]
        public void IncreaseQuantity_Set_Minus_10_Should_Return_NegativeValueException()
        {
            #region Arrange

            var merchItem = new MerchItem(new Sku(1), new Name("test"), new Item(ItemType.Bag),
                new Quantity(10));

            #endregion

            #region Act & Assert

            Assert.Throws<NegativeValueException>(() => merchItem.IncreaseQuantity(-10));

            #endregion
        }

        [Fact]
        public void GiveOutItems_Set_10_Should_Return_Zero()
        {
            #region Arrange

            var merchItem = new MerchItem(new Sku(1), new Name("test"), new Item(ItemType.Bag),
                new Quantity(10));

            #endregion

            #region Act

            merchItem.GiveOutItems(10);

            #endregion

            #region Assert

            Assert.Equal(0, merchItem.Quantity.Value);

            #endregion
        }

        [Fact]
        public void GiveOutItems_Set_Minus_10_Should_Return_NegativeValueException()
        {
            #region Arrange

            var merchItem = new MerchItem(new Sku(1), new Name("test"), new Item(ItemType.Bag),
                new Quantity(10));

            #endregion

            #region Act & Assert

            Assert.Throws<NegativeValueException>(() => merchItem.GiveOutItems(-10));
            
            #endregion
        }

        [Fact]
        public void GiveOutItems_Set_20_Should_Return_NotEnoughItemsException()
        {
            #region Arrange

            var merchItem = new MerchItem(new Sku(1), new Name("test"), new Item(ItemType.Bag),
                new Quantity(10));

            #endregion

            #region Act & Assert

            Assert.Throws<NotEnoughItemsException>(() => merchItem.GiveOutItems(20));
            
            #endregion
        }
    }
}