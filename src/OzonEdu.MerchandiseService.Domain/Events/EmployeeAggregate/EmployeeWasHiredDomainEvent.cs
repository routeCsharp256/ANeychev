using MediatR;

namespace OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate
{
    public class EmployeeWasHiredDomainEvent : INotification
    {
        public long EmployeeId { get; set; }
    }
}