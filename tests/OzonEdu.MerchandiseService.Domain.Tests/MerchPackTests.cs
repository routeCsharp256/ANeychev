using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public class MerchPackTests
    {
        [Fact]
        public void MerchPack_Init_Should_Return_Valid_MerchPack()
        {
            #region Arrange

            var merchPackType = MerchPackType.WelcomePack;
            var merchPackItems = new List<MerchItem>
            {
                new(new Sku(1), new Name("test1"), new Item(ItemType.Bag), new Quantity(10)),
                new(new Sku(2), new Name("test2"), new Item(ItemType.Notepad), new Quantity(20))
            };

            #endregion

            #region Act
            
            var merchPack = new MerchPack(merchPackType, merchPackItems);

            #endregion

            #region Act
            
            Assert.Equal(merchPackType, merchPack.Type);
            Assert.Equal(merchPackItems, merchPack.Items);

            #endregion
        }

        [Fact]
        public void MerchPack_Set_MerchType_Null_Should_Return_MerchTypeNullException()
        {
            Assert.Throws<MerchTypeNullException>(() => new MerchPack(
                null,
                new List<MerchItem>
                {
                    new(new Sku(1), new Name("test1"), new Item(ItemType.Bag), new Quantity(10)),
                    new(new Sku(2), new Name("test2"), new Item(ItemType.Notepad), new Quantity(20))
                }));
        }

        [Fact]
        public void MerchPack_Set_MerchType_Null_Should_Return_ListMerchItemsNullException()
        {
            Assert.Throws<ListMerchItemsNullException>(() => new MerchPack(MerchPackType.WelcomePack, null));
        }

        [Fact]
        public void MerchPack_Set_MerchType_Null_Should_Return_ListMerchItemsCountZeroException()
        {
            Assert.Throws<ListMerchItemsCountZeroException>(() =>
                new MerchPack(MerchPackType.WelcomePack, new List<MerchItem>()));
        }
    }
}