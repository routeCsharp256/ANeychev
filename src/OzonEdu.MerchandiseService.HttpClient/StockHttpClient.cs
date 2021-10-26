using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.HttpClient
{
    public interface IStockHttpClient
    {
        Task<List<StockItemResponse>> V1GetAll(CancellationToken token);
    }

    public class StockHttpClient : IStockHttpClient
    {
        private readonly System.Net.Http.HttpClient _httpClient;

        public StockHttpClient(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StockItemResponse>> V1GetAll(CancellationToken token)
        {
            using var response = await _httpClient.GetAsync("v1/api/stocks", token);
            var body = await response.Content.ReadAsStringAsync(token);
            return JsonSerializer.Deserialize<List<StockItemResponse>>(body);
        }
    }
}