using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces
{
    public interface IEmployeeHttpClient
    {
        Task<List<EmployeeItemResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}