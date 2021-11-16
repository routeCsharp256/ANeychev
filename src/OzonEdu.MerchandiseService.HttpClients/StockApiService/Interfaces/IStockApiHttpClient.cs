using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.HttpClients.StockApiService.Interfaces
{
    public interface IStockApiHttpClient
    {
        /// <summary>
        /// Запрашивает наличие мерча
        /// </summary>
        /// <param name="sku">Товарная позиция, единица складского учёта</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns></returns>
        Task<int> GetAvailableQuantity(long sku, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Запрашивает выдачу мерча 
        /// </summary>
        /// <param name="sku">Товарная позиция, единица складского учёта</param>
        /// <param name="quantity">Количество</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns></returns>
        Task GiveOut(long sku, int quantity, CancellationToken cancellationToken = default);
    }
}