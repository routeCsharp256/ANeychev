using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces;
using OzonEdu.MerchandiseService.HttpClients.StockApiService.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Services
{
    public sealed class ApplicationService : IApplicationService
    {
        private readonly IStockApiHttpClient _stockApiHttpClient;
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IEmployeeHttpClient _employeeHttpClient;
        private readonly IEmployeeRepository _employeeRepository;

        public ApplicationService(IStockApiHttpClient stockApiHttpClient,
            IMerchRequestRepository merchRequestRepository,
            IMerchPackRepository merchPackRepository, 
            IEmployeeHttpClient employeeHttpClient, 
            IEmployeeRepository employeeRepository)
        {
            _stockApiHttpClient = stockApiHttpClient ?? throw new ArgumentNullException(nameof(stockApiHttpClient));
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository));
            _employeeHttpClient = employeeHttpClient ?? throw new ArgumentNullException(nameof(employeeHttpClient));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<Employee> GetEmployeeAsync(long employeeId, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeRepository.FindByIdAsync(employeeId, cancellationToken);
            if (employee is not null) return employee;
            var externalEmployees = await _employeeHttpClient.GetAllAsync(cancellationToken);
            var externalEmployee = externalEmployees.FirstOrDefault(x => x.id == employeeId);
            if (externalEmployee is null)
                throw new Exception($"Employee with id {employeeId} doesn't exist");
            var createEmployee = new Employee(new EmployeeFirstName(externalEmployee.firstName),
                new EmployeeMiddleName(externalEmployee.middleName),
                new EmployeeLastName(externalEmployee.lastName),
                new BirthDay(externalEmployee.birthDay),
                new HiringDate(externalEmployee.hiringDate),
                Email.Create(externalEmployee.email));
            await _employeeRepository.CreateAsync(createEmployee, cancellationToken);
            await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            employee = await _employeeRepository.FindByIdAsync(createEmployee.Id, cancellationToken);

            return employee;
        }

        public async Task<MerchPack> GetMerchPackAsync(int merchPackTypeId,
            CancellationToken cancellationToken = default)
        {
            var merchPack = await _merchPackRepository.FindByIdAsync(merchPackTypeId, cancellationToken);
            if (merchPack is null) throw new Exception($"Merch pack with id {merchPackTypeId} doesn't exist");
            return merchPack;
        }

        public async Task<bool> CheckRepeatedMerchRequestAsync(Employee employee, MerchPack merchPack,
            CancellationToken cancellationToken = default)
        {
            var repeatMerckPacks = (await _merchRequestRepository.GetByEmployeeIdAsync(employee.Id, cancellationToken)).ToList()
                .FindAll(x =>
                    Equals(x.Type, merchPack.Type) &&
                    Equals(x.Status, RequestStatus.Done) &&
                    x.IsExpiredDateOfGiveOut(DateTime.Today).Days < 365);

            return repeatMerckPacks.Count != 0;
        }

        public async Task<MerchRequest> CreateMerchRequestAsync(Employee employee, MerchPack merchPack,
            CancellationToken cancellationToken = default)
        {
            var merchRequest =
                new MerchRequest(employee.Id, employee.Email,merchPack.Type, new List<MerchItem>(merchPack.Items));
            await _merchRequestRepository.CreateAsync(merchRequest, cancellationToken);
            await _merchRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return merchRequest;
        }

        public async Task TryGiveOutMerchRequestAsync(MerchRequest merchRequest,
            CancellationToken cancellationToken = default)
        {
            var reserveMerchItems = new List<MerchItem>();

            foreach (var merchItem in merchRequest.Items)
            {
                var stockQuantity =
                    await _stockApiHttpClient.GetAvailableQuantity(merchItem.Sku.Value, cancellationToken);
                if (merchItem.Quantity.Value == stockQuantity) reserveMerchItems.Add(merchItem);
            }

            if (merchRequest.Items.Count != reserveMerchItems.Count) return;

            foreach (var merchItem in merchRequest.Items)
                await _stockApiHttpClient.GiveOut(merchItem.Sku.Value, merchItem.Quantity.Value, cancellationToken);

            merchRequest.Complete(DateTime.Today);
            await _merchRequestRepository.UpdateAsync(merchRequest, cancellationToken);
            await _merchRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}