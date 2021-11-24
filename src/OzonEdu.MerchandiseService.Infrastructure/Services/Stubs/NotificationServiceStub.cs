using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Services.Stubs
{
    public class NotificationServiceStub : INotificationService
    {
        public async Task SendMessageAsync(Email employeeEmail, MerchType merchType, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task SendMessageAsync(Email managerEmail, string message, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}