using System.Collections.Generic;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public sealed class MerchPackTests
    {
        [Fact]
        public void MerchPack_Init_Should_Return_Valid_MerchPack()
        {
            var merchPackType = MerchPackType.WelcomePack;
            var merchPackItems = new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10)),
                new(new Item(ItemType.Notepad), new Quantity(20))
            };
            var merchPack = new MerchPack(merchPackType, merchPackItems);
            Assert.Equal(merchPackType, merchPack.Type);
            Assert.Equal(merchPackItems, merchPack.Items);
        }

        [Fact]
        public void MerchPack_Set_MerchType_Null_Should_Return_MerchTypeNullException()
        {
            Assert.Throws<MerchTypeNullException>(() => new MerchPack(
                null,
                new List<MerchItem>
                {
                    new(new Item(ItemType.Bag), new Quantity(10)),
                    new(new Item(ItemType.Notepad), new Quantity(20))
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