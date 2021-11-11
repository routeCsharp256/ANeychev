using MediatR;

namespace OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate
{
    public class EmployeeBecameVeteranDomainEvent : INotification
    {
        public long EmployeeId { get; set; }
    }
}