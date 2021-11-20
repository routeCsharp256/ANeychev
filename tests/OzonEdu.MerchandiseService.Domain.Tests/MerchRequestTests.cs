using System;
using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Exceptions;
using OzonEdu.MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace OzonEdu.MerchandiseService.Domain.Tests
{
    public sealed class MerchRequestTests
    {
        [Fact]
        public void Create_Set_Status_InProgress_Should_Return_StatusRequestException()
        {
            var merchRequest = new MerchRequest(1, Email.Create("test@test.com"), MerchPackType.VeteranPack);

            Assert.Throws<StatusRequestException>(() => merchRequest.Create(1, Email.Create("test@test.com")));
        }

        [Fact]
        public void Constructor_EmployeeId_Set_Zero_Should_Return_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new MerchRequest(0, Email.Create("test@test.com"), MerchPackType.VeteranPack));
        }

        [Fact]
        public void Constructor_Email_Set_Null_Should_Return_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(1, null, MerchPackType.VeteranPack));
        }

        [Fact]
        public void Constructor_Type_Set_Null_Should_Return_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new MerchRequest(1, Email.Create("test@test.com"), null));
        }

        [Fact]
        public void Constructor_List_MerchItems_Set_Null_Should_Return_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new MerchRequest(1, Email.Create("test@test.com"), MerchPackType.VeteranPack, null));
        }

        [Fact]
        public void Constructor_List_MerchItems_Set_Count_Zero_Should_Return_ListMerchItemsCountZeroException()
        {
            Assert.Throws<ListMerchItemsCountZeroException>(() =>
                new MerchRequest(1, Email.Create("test@test.com"), MerchPackType.VeteranPack, new List<MerchItem>()));
        }

        [Fact]
        public void Create_EmployeeId_Set_Zero_Should_Return_ArgumentException()
        {
            var merchRequest = new MerchRequest();

            Assert.Throws<ArgumentException>(() => merchRequest.Create(0, Email.Create("test@test.com")));
        }

        [Fact]
        public void Create_Email_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchRequest = new MerchRequest();

            Assert.Throws<ArgumentNullException>(() => merchRequest.Create(1, null));
        }

        [Fact]
        public void StarkWork_Type_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchRequest = new MerchRequest();
            merchRequest.Create(1, Email.Create("test@test.com"));

            Assert.Throws<ArgumentNullException>(() => merchRequest.StartWork(null, new List<MerchItem>
            {
                new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
            }));
        }

        [Fact]
        public void StarkWork_List_MerchItems_Set_Null_Should_Return_ArgumentNullException()
        {
            var merchRequest = new MerchRequest();
            merchRequest.Create(1, Email.Create("test@test.com"));

            Assert.Throws<ArgumentNullException>(() => merchRequest.StartWork(MerchPackType.VeteranPack, null));
        }

        [Fact]
        public void StarkWork_List_MerchItems_Set_Count_Zero_Should_Return_ListMerchItemsCountZeroException()
        {
            var merchRequest = new MerchRequest();
            merchRequest.Create(1, Email.Create("test@test.com"));

            Assert.Throws<ListMerchItemsCountZeroException>(() => merchRequest.StartWork(MerchPackType.VeteranPack,
                new List<MerchItem>()));
        }

        [Fact]
        public void StartWork_Set_Status_Draft_Should_Return_StatusRequestException()
        {
            var merchRequest = new MerchRequest();

            Assert.Throws<StatusRequestException>(() => merchRequest.StartWork(MerchPackType.VeteranPack,
                new List<MerchItem>
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
            merchRequest.Create(1, Email.Create("test@test.com"));
            merchRequest.StartWork(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
            });
            merchRequest.Complete(DateTime.Today);
            
            Assert.Equal(
                @"OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate.MerchRequestWasDoneDomainEvent",
                merchRequest.DomainEvents.ToList()[0].ToString());
        }

        [Fact]
        public void Complete_Set_Status_Draft_Should_Return_StatusRequestException()
        {
            var merchRequest = new MerchRequest();
            Assert.Throws<StatusRequestException>(() => merchRequest.Complete(DateTime.Today));
        }

        [Fact]
        public void IsExpiredDateOfGiveOut_Set_Status_Draft_Should_Return_StatusRequestException()
        {
            var merchRequest = new MerchRequest();
            Assert.Throws<StatusRequestException>(() => merchRequest.IsExpiredDateOfGiveOut(DateTime.Today));
        }

        [Fact]
        public void IsExpiredDateOfGiveOut_Set_Invalid_Date_Should_Return_ArgumentException()
        {
            var merchRequest = new MerchRequest();
            merchRequest.Create(1, Email.Create("test@test.com"));
            merchRequest.StartWork(MerchPackType.VeteranPack, new List<MerchItem>
            {
                new(new Sku(1), new Name("test"), new Item(ItemType.Bag), new Quantity(10))
            });
            merchRequest.Complete(DateTime.Today);
            Assert.Throws<StatusRequestException>(() => merchRequest.IsExpiredDateOfGiveOut(DateTime.Today.AddDays(-1)));
        }
    }
}