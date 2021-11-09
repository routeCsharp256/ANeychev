using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.Events.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.MerchPackAggregate
{
    public class ReachedMinimumMerchItemsNumberDomainEventHandler : INotificationHandler<ReachedMinimumMerchItemsNumberDomainEvent>
    {
        public async Task Handle(ReachedMinimumMerchItemsNumberDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}