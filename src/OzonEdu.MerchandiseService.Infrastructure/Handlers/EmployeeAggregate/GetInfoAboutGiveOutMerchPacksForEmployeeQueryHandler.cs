using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries.EmployeeAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.EmployeeAggregate
{
    public sealed class GetInfoAboutGiveOutMerchPacksForEmployeeQueryHandler
        : IRequestHandler<GetInfoAboutGiveOutMerchPacksForEmployeeQuery, List<MerchItem>>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;
        private readonly IApplicationService _applicationService;

        public GetInfoAboutGiveOutMerchPacksForEmployeeQueryHandler(IMerchRequestRepository merchRequestRepository,
            IApplicationService applicationService)
        {
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        }

        public async Task<List<MerchItem>> Handle(GetInfoAboutGiveOutMerchPacksForEmployeeQuery request,
            CancellationToken cancellationToken = default)
        {
            var result = new List<MerchItem>();
            var employee = await _applicationService.GetEmployeeAsync(request.EmployeeId, cancellationToken);

            var requests =
                await _merchRequestRepository.GetByEmployeeIdAsync(employee.Id, cancellationToken);
            
            if (requests is null) return result;
            
            foreach (var merchRequest in requests) result.AddRange(merchRequest.Items);
            return result;
        }
    }
}