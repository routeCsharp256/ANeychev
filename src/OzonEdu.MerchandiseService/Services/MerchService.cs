using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Services
{
    public sealed class MerchService : IMerchService
    {
        public async Task<List<MerchItemResponse>> GetInfoAsync(long employeeId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> GetMerchPackAsync(long employeeId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}