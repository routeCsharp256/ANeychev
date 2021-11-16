using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.EmployeeAggregate
{
    public sealed class EmployeeWasDismissedDomainEventHandler : INotificationHandler<EmployeeWasDismissedDomainEvent>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchRequestRepository _merchRequestRepository;

        public EmployeeWasDismissedDomainEventHandler(IEmployeeRepository employeeRepository,
            IMerchRequestRepository merchRequestRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));
        }

        public async Task Handle(EmployeeWasDismissedDomainEvent notification,
            CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.FindByIdAsync(notification.EmployeeId, cancellationToken);
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            var merchRequests = (await _merchRequestRepository.GetByEmployeeIdAsync(employee.Id, cancellationToken)).ToList()
                .FindAll(x => Equals(x.Status, RequestStatus.InProgress));

            if (merchRequests.Count > 0)
            {
                foreach (var merchRequest in merchRequests)
                {
                    merchRequest.Cancel();
                    await _merchRequestRepository.UpdateAsync(merchRequest, cancellationToken);
                }

                await _merchRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
        }
    }
}