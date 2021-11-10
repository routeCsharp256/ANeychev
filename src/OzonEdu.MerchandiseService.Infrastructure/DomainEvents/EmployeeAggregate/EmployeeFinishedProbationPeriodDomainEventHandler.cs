using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.EmployeeAggregate
{
    public sealed class EmployeeFinishedProbationPeriodDomainEventHandler
        : INotificationHandler<EmployeeFinishedProbationPeriodDomainEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchPackRepository _merchPackRepository;

        public EmployeeFinishedProbationPeriodDomainEventHandler(IEmployeeRepository employeeRepository,
            IMerchPackRepository merchPackRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository));
        }

        public async Task Handle(EmployeeFinishedProbationPeriodDomainEvent notification,
            CancellationToken cancellationToken = default)
        {
            // TODO Проверить наличие заявки на выдачу ProbationPeriodEndingPack
            // TODO Если ProbationPeriodEndingPack не выдавался, то создать заявку на выдачу этого набора мерча
            throw new System.NotImplementedException();
        }
    }
}