using MediatR;

namespace OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate
{
    public class EmployeeFinishedProbationPeriodDomainEvent : INotification
    {
        public long EmployeeId { get; set; }
    }
}