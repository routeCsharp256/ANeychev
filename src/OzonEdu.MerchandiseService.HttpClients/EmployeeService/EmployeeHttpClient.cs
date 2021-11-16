using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.HttpClients.EmployeeService
{
    public sealed class EmployeeHttpClient : IEmployeeHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public EmployeeHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeItemResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.GetAsync("/api/employees/getall", cancellationToken);
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<List<EmployeeItemResponse>>(body);
        }
    }
}