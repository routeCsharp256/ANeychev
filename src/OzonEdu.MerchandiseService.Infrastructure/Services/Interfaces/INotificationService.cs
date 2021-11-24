using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendMessageAsync(Email employeeEmail, MerchType merchType, CancellationToken cancellationToken = default);
        
        Task SendMessageAsync(Email managerEmail, string message, CancellationToken cancellationToken = default);
    }
}