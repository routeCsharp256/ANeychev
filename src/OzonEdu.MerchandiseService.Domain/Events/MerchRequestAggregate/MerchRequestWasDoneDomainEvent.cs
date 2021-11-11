using System;
using MediatR;

namespace OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate
{
    public class MerchRequestWasDoneDomainEvent : INotification
    {
        public Guid RequestNumber { get; set; }
    }
}