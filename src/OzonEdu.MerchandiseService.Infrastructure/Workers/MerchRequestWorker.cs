using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Workers
{
    public sealed class MerchRequestWorker : IMerchRequestWorker
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IApplicationService _applicationService;
        // ====
        private readonly IEmployeeHttpClient _employeeHttpClient;

        public MerchRequestWorker(IMerchRequestRepository merchRequestRepository,
            IEmployeeRepository employeeRepository,
            IEmployeeHttpClient employeeHttpClient,
            IApplicationService applicationService)
        {
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));

            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _employeeHttpClient = employeeHttpClient ?? throw new ArgumentNullException(nameof(employeeHttpClient));
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        }

        public async Task AutoGenerateMerckRequests(CancellationToken cancellationToken = default)
        {
            var externalEmployees = await _employeeHttpClient.GetAllAsync(cancellationToken) ??
                                    throw new ArgumentException("External employee doesn't exist");

            foreach (var externalEmployee in externalEmployees)
            {
                var employee =
                    await _employeeRepository.FindByEmailAsync(Email.Create(externalEmployee.email), cancellationToken);
                if (employee is null)
                {
                    var createEmployee = new Employee(new EmployeeFirstName(externalEmployee.firstName),
                        new EmployeeMiddleName(externalEmployee.middleName),
                        new EmployeeLastName(externalEmployee.lastName),
                        new BirthDay(externalEmployee.birthDay),
                        new HiringDate(externalEmployee.hiringDate),
                        Email.Create(externalEmployee.email));
                    await _employeeRepository.CreateAsync(createEmployee, cancellationToken);
                    await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                    employee = await _employeeRepository.FindByIdAsync(createEmployee.Id, cancellationToken);
                }

                employee.CheckWorkExperience(DateTime.Today);
            }
        }

        public async Task TryGiveOutMerchPacks(CancellationToken cancellationToken = default)
        {
            var merchRequests = await _merchRequestRepository.GetAllAsync(cancellationToken);
            if (merchRequests is null) return;
            var actualMerchRequests = merchRequests.ToList().FindAll(x => Equals(x.Status, RequestStatus.InProgress));
            if (actualMerchRequests.Count == 0) return;

            foreach (var actualMerchRequest in actualMerchRequests)
                await _applicationService.TryGiveOutMerchRequestAsync(actualMerchRequest, cancellationToken);
        }
    }
}