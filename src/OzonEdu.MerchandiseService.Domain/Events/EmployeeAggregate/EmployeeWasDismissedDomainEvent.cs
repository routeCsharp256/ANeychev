using MediatR;

namespace OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate
{
    public class EmployeeWasDismissedDomainEvent : INotification
    {
        public long EmployeeId { get; set; }
    }
}