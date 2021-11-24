using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.Events.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.MerchPackAggregate
{
    public sealed class SupplyArrivedWithStockItemsDomainEventHandler
        : INotificationHandler<SupplyArrivedWithStockItemsDomainEvent>
    {
        private readonly IMerchRequestWorker _merchRequestWorker;

        public SupplyArrivedWithStockItemsDomainEventHandler(IMerchRequestWorker merchRequestWorker) =>
            _merchRequestWorker = merchRequestWorker ?? throw new ArgumentNullException(nameof(merchRequestWorker));

        public async Task Handle(SupplyArrivedWithStockItemsDomainEvent notification,
            CancellationToken cancellationToken = default) =>
            await _merchRequestWorker.TryGiveOutMerchPacks(cancellationToken);
    }
}