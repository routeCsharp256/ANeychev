using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.EmployeeAggregate
{
    public sealed class EmployeeWasDismissedDomainEventHandler : INotificationHandler<EmployeeWasDismissedDomainEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchPackRepository _merchPackRepository;

        public EmployeeWasDismissedDomainEventHandler(IEmployeeRepository employeeRepository,
            IMerchPackRepository merchPackRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository));
        }

        public async Task Handle(EmployeeWasDismissedDomainEvent notification,
            CancellationToken cancellationToken = default)
        {
            // TODO Аннулировать все не завершенные заявки уволенного сотрудника
            throw new System.NotImplementedException();
        }
    }
}