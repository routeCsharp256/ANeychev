using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<MerchPack> GetMerchPackAsync(int merchTypeId, CancellationToken cancellationToken = default);

        Task<bool> IsNotRepeatedMerchRequestAsync(long employeeId, MerchPack merchPack,
            CancellationToken cancellationToken = default);

        Task<MerchRequest> CreateMerchRequestAsync(long employeeId, ClothingSize clothingSize, Email employeeEmail,
            Email managerEmail, MerchPack merchPack, CancellationToken cancellationToken = default);

        Task TryGiveOutMerchRequestAsync(MerchRequest merchRequest, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<MerchRequest>> FindMerchRequestsAsync(Func<MerchRequest, bool> predicate,
            CancellationToken cancellationToken = default);
    }
}