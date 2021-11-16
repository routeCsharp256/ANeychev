using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Contracts;

namespace OzonEdu.MerchandiseService.Infrastructure.Stubs
{
    public sealed class EmployeeRepository : IEmployeeRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        
        public async Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Employee> FindByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}