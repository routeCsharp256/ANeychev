using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OzonEdu.MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Workers
{
    public sealed class MerchRequestWorker : IMerchRequestWorker
    {
        private readonly ILogger<MerchRequestWorker> _logger;
        private readonly IMerchPackRepository _merchPackRepository;
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public MerchRequestWorker(ILogger<MerchRequestWorker> logger,
            IMerchPackRepository merchPackRepository,
            IMerchRequestRepository merchRequestRepository,
            IEmployeeRepository employeeRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _merchPackRepository = merchPackRepository ?? throw new ArgumentNullException(nameof(merchPackRepository));
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public Task AutoGenerateMerckRequest(CancellationToken cancellationToken = default)
        {
            //TODO Добавить логику автоматической генерации заявок на выдачу наборов мерча
            return Task.CompletedTask;
        }
    }
}