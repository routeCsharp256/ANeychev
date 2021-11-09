using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.EmployeeAggregate
{
    public class EmployeeBecameVeteranDomainEventHandler : INotificationHandler<EmployeeBecameVeteranDomainEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchPackRepository _merchPackRepository;

        public EmployeeBecameVeteranDomainEventHandler(IEmployeeRepository employeeRepository,
            IMerchPackRepository merchPackRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository ));
        }
        
        public async Task Handle(EmployeeBecameVeteranDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO Проверить наличие заявки на выдачу VeteranPack
            // TODO Если VeteranPack не выдавался, то создать заявку на выдачу этого набора мерча
            throw new System.NotImplementedException();
        }
    }
}