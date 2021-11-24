using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchandiseService.Domain.Contracts;
using OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;
using OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Workers
{
    public sealed class MerchRequestWorker : IMerchRequestWorker, IDisposable
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IApplicationService _applicationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeHttpClient _employeeHttpClient;

        public MerchRequestWorker(IMerchRequestRepository merchRequestRepository,
            IEmployeeHttpClient employeeHttpClient,
            IApplicationService applicationService,
            IUnitOfWork unitOfWork)
        {
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));
            _employeeHttpClient = employeeHttpClient ?? throw new ArgumentNullException(nameof(employeeHttpClient));
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task AutoGenerateMerckRequests(CancellationToken cancellationToken = default)
        {
            var employees = await _employeeHttpClient.GetAllAsync(cancellationToken) ??
                            throw new ArgumentException("External employee doesn't exist");

            foreach (var employee in employees)
            {
                var merchPackType = CheckWorkExperience(employee.hiringDate);
                if (merchPackType is null) continue;
                var merchPack = await _applicationService.GetMerchPackAsync(merchPackType.Id, cancellationToken);
                
                //TODO 1. Откуда брать почту ответственного менеджера при автоматической генерации заявок?
                //TODO 2. Откуда брать размер сотрудника при автоматической генерации заявок?
                
                if (await _applicationService.IsNotRepeatedMerchRequestAsync(employee.id, merchPack, cancellationToken))
                    await _applicationService.CreateMerchRequestAsync(employee.id, ClothingSize.XXL,
                        Email.Create(employee.email), Email.Create("resposible_manager@mail.com"), merchPack,
                        cancellationToken);
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

        //TODO добавить автоматическое назначение ответственного менеджера

        private static MerchPackType? CheckWorkExperience(DateTimeOffset hiringDate)
        {
            var today = DateTimeOffset.UtcNow;
            if (today.CompareTo(hiringDate) > 0) throw new ArgumentException(nameof(hiringDate));

            var calcWorkExperience =
                (hiringDate.Year * 12 + hiringDate.Month) - (today.Year * 12 + today.Month);
            return calcWorkExperience switch
            {
                0 => MerchPackType.WelcomePack,
                4 => MerchPackType.ProbationPeriodEndingPack,
                61 => MerchPackType.VeteranPack,
                _ => null
            };
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}