using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.MerchRequestAggregate
{
    public class MerchRequestWasDoneDomainEventHandler : INotificationHandler<MerchRequestWasDoneDomainEvent>
    {
        public async Task Handle(MerchRequestWasDoneDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO отправить уведомление сотруднику о получении набора мерча на складе
            throw new System.NotImplementedException();
        }
    }
}