using System;
using MediatR;

namespace OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate
{
    public sealed class MerchRequestWasDoneDomainEvent : INotification
    {
        public Guid RequestNumber { get; set; }
    }
}