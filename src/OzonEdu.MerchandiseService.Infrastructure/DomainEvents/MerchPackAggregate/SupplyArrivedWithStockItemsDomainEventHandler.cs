using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.Events.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.MerchPackAggregate
{
    public class SupplyArrivedWithStockItemsDomainEventHandler : INotificationHandler<SupplyArrivedWithStockItemsDomainEvent>
    {
        public async Task Handle(SupplyArrivedWithStockItemsDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}