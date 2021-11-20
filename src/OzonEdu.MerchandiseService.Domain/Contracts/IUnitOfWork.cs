using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchandiseService.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask StartTransaction(CancellationToken cancellationToken = default);
        
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}