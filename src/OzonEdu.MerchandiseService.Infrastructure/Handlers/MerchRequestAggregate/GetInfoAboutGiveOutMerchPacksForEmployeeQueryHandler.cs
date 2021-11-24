using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries;
using OzonEdu.MerchandiseService.Infrastructure.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchRequestAggregate
{
    public sealed class GetInfoAboutGiveOutMerchPacksForEmployeeQueryHandler
        : IRequestHandler<GetInfoAboutGiveOutMerchPacksForEmployeeQuery, List<MerchRequestItem>>
    {
        private readonly IApplicationService _applicationService;

        public GetInfoAboutGiveOutMerchPacksForEmployeeQueryHandler(IApplicationService applicationService)
        {
            _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        }

        public async Task<List<MerchRequestItem>> Handle(GetInfoAboutGiveOutMerchPacksForEmployeeQuery request,
            CancellationToken cancellationToken = default)
        {
            var result = new List<MerchRequestItem>();

            var requests = await _applicationService.FindMerchRequestsAsync(it =>
                it.EmployeeId == request.EmployeeId, cancellationToken);

            if (requests is null) return result;
            
            foreach (var merchRequest in requests) result.AddRange(merchRequest.Items);
            return result;
        }
    }
}