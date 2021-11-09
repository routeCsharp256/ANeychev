using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.MerchRequestAggregate
{
    public class MerchRequestWasCanceledDomainEventHandler : INotificationHandler<MerchRequestWasCanceledDomainEvent>
    {
        public async Task Handle(MerchRequestWasCanceledDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO Отправить уведомление в Stock API c отменой резервирования
            throw new System.NotImplementedException();
        }
    }
}