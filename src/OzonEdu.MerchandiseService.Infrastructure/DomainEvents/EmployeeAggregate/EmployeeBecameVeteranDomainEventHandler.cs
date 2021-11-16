using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using MediatR;
using OzonEdu.MerchandiseService.Domain.Events.EmployeeAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.DomainEvents.EmployeeAggregate
{
    public sealed class EmployeeBecameVeteranDomainEventHandler : INotificationHandler<EmployeeBecameVeteranDomainEvent>
    {
        private readonly IApplicationService _applicationService;

        public EmployeeBecameVeteranDomainEventHandler(IApplicationService applicationService) =>
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));

        public async Task Handle(EmployeeBecameVeteranDomainEvent notification,
            CancellationToken cancellationToken = default)
        {
            var employee = await _applicationService.GetEmployeeAsync(notification.EmployeeId, cancellationToken);
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            var merchPack = await _applicationService.GetMerchPackAsync((int) MerchType.VeteranPack, cancellationToken);
            if (await _applicationService.CheckRepeatedMerchRequestAsync(employee, merchPack, cancellationToken))
                return;
            var merchRequest =
                await _applicationService.CreateMerchRequestAsync(employee, merchPack, cancellationToken);
            await _applicationService.TryGiveOutMerchRequestAsync(merchRequest, cancellationToken);
        }
    }
}