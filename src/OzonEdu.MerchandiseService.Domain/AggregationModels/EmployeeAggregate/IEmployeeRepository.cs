using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Найти сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сотрудника</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns>Сотрудник</returns>
        Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken =default);

        /// <summary>
        /// Найти сотрудника по электронной почте
        /// </summary>
        /// <param name="email">Адрес электронной почты сотрудника</param>
        /// <param name="cancellationToken">Токен для отмены операции</param>
        /// <returns></returns>
        Task<Employee> FindByEmailAsync(Email email, CancellationToken cancellationToken = default);
    }
}