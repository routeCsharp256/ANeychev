using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Employee> GetEmployeeAsync(long employeeId, CancellationToken cancellationToken = default);
        Task<MerchPack> GetMerchPackAsync(int merchPackTypeId, CancellationToken cancellationToken = default);

        Task<bool> CheckRepeatedMerchRequestAsync(Employee employee, MerchPack merchPack,
            CancellationToken cancellationToken = default);

        Task<MerchRequest> CreateMerchRequestAsync(Employee employee, MerchPack merchPack,
            CancellationToken cancellationToken = default);

        Task TryGiveOutMerchRequestAsync(MerchRequest merchRequest, CancellationToken cancellationToken = default);
    }
}