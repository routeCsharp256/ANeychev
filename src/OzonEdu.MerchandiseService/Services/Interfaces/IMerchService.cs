using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.Services.Interfaces
{
    public interface IMerchService
    {
        Task<List<MerchItemResponse>> GetInfoAsync(long employeeId, CancellationToken cancellationToken);

        Task GiveOutAsync(long employeeId, CancellationToken cancellationToken);
    }
}