using System;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public sealed class MerchRequestTests
    {
        [Fact]
        public void Create_Set_Status_InProgress_Should_Return_StatusRequestException()
        {
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });
            var merchRequest = new MerchRequest(1, ClothingSize.L, Email.Create("test@test.com"),
                Email.Create("test@test.com"),
                merchPack, new List<MerchRequestItem>
                {
                    new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
                });
            Assert.Throws<StatusRequestException>(() =>
                merchRequest.Create(1, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com")));
        }

        [Fact]
        public void Constructor_EmployeeId_Set_Zero_Should_Return_ArgumentException()
        {
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });
            Assert.Throws<ArgumentException>(() =>
                new MerchRequest(0, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com"),
                    merchPack, new List<MerchRequestItem>
                    {
                        new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
                    }));
        }

        [Fact]
        public void Constructor_Email_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(1, ClothingSize.L, null,
                Email.Create("test@test.com"), merchPack, new List<MerchRequestItem>
                {
                    new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
                }));
        }

        [Fact]
        public void Constructor_MerchPack_Set_Null_Should_Return_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(1, ClothingSize.L,
                Email.Create("test@test.com"), Email.Create("test@test.com"), null, new List<MerchRequestItem>
                {
                    new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
                }));
        }

        [Fact]
        public void Constructor_List_MerchRequestItems_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });
            Assert.Throws<ArgumentNullException>(() =>
                new MerchRequest(1, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com"),
                    merchPack, null));
        }

        [Fact]
        public void Constructor_List_MerchRequestItems_Set_Count_Zero_Should_Return_ListMerchItemsCountZeroException()
        {
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });
            Assert.Throws<ListMerchItemsCountZeroException>(() =>
                new MerchRequest(1, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com"),
                    merchPack, new List<MerchRequestItem>()));
        }

        [Fact]
        public void Create_EmployeeId_Set_Zero_Should_Return_ArgumentException()
        {
            var merchRequest = new MerchRequest();

            Assert.Throws<ArgumentException>(() =>
                merchRequest.Create(0, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com")));
        }

        [Fact]
        public void Create_Email_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchRequest = new MerchRequest();

            Assert.Throws<ArgumentNullException>(() =>
                merchRequest.Create(1, ClothingSize.L, null, Email.Create("test@test.com")));
        }

        [Fact]
        public void StarkWork_Type_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchRequest = new MerchRequest();
            merchRequest.Create(1, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com"));

            Assert.Throws<ArgumentNullException>(() => merchRequest.StartWork(null, new List<MerchRequestItem>
            {
                new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
            }));
        }

        [Fact]
        public void StarkWork_List_MerchItems_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchRequest = new MerchRequest();
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });
            merchRequest.Create(1, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com"));

            Assert.Throws<ArgumentNullException>(() => merchRequest.StartWork(merchPack, null));
        }

        [Fact]
        public void StarkWork_List_MerchItems_Set_Count_Zero_Should_Return_ListMerchItemsCountZeroException()
        {
            var merchRequest = new MerchRequest();
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });

            merchRequest.Create(1, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com"));

            Assert.Throws<ListMerchItemsCountZeroException>(() => merchRequest.StartWork(merchPack,
                new List<MerchRequestItem>()));
        }

        [Fact]
        public void StartWork_Set_Status_Draft_Should_Return_StatusRequestException()
        {
            var merchRequest = new MerchRequest();
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });

            Assert.Throws<StatusRequestException>(() => merchRequest.StartWork(merchPack,
                new List<MerchRequestItem>
                {
                    new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
                }));
        }

        [Fact]
        public void Cancel_Set_Status_Draft_Should_Return_StatusRequestException()
        {
            var merchRequest = new MerchRequest();
            Assert.Throws<StatusRequestException>(() => merchRequest.Cancel());
        }

        [Fact]
        public void MerchRequestWasDoneDomainEvent_Trigger_Complete()
        {
            var merchRequest = new MerchRequest();
            var merchPack = new MerchPack(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Item(ItemType.Bag), new Quantity(10))
            });
            merchRequest.Create(1, ClothingSize.L, Email.Create("test@test.com"), Email.Create("test@test.com"));
            merchRequest.StartWork(merchPack, new List<MerchRequestItem>
            {
                new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
            });
            merchRequest.Complete();

            Assert.Equal(
                @"OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate.MerchRequestWasDoneDomainEvent",
                merchRequest.DomainEvents.ToList()[0].ToString());
        }

        [Fact]
        public void Complete_Set_Status_Draft_Should_Return_StatusRequestException()
        {
            var merchRequest = new MerchRequest();
            Assert.Throws<StatusRequestException>(() => merchRequest.Complete());
        }
    }
}