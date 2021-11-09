using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.EmployeeAggregate
{
    public class EmployeeWasHiredDomainEventHandler : INotificationHandler<EmployeeWasHiredDomainEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchPackRepository _merchPackRepository;

        public EmployeeWasHiredDomainEventHandler(IEmployeeRepository employeeRepository,
            IMerchPackRepository merchPackRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository ));
        }

        public async Task Handle(EmployeeWasHiredDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO Проверить наличие заявок на WelcomePack, если заявка отсутствует, то создать её.
            throw new System.NotImplementedException();
        }
    }
}