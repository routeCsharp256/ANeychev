using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    public interface IMerchPackRepository : IRepository<MerchPack>
    {
        /// <summary>
        /// Найти товарную позицию по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор товарной позиции</param>
        /// <param name="cancellationToken">Токен для отмены операции. <see cref="CancellationToken"/></param>
        /// <returns>Товарная позиция</returns>
        Task<MerchPack> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}