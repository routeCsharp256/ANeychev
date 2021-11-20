using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
    }
}