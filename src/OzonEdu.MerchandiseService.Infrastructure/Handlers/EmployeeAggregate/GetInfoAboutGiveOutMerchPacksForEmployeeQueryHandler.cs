using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.EmployeeAggregate
{
    public sealed class GetInfoAboutGiveOutMerchPacksForEmployeeQueryHandler
        : IRequestHandler<GetInfoAboutGiveOutMerchPacksForEmployeeQuery, List<MerchItem>>
    {
        private readonly IMerchRequestRepository _merchRequestRepository;

        public GetInfoAboutGiveOutMerchPacksForEmployeeQueryHandler(IMerchRequestRepository merchRequestRepository) =>
            _merchRequestRepository =
                merchRequestRepository ?? throw new ArgumentNullException(nameof(merchRequestRepository));

        public async Task<List<MerchItem>> Handle(GetInfoAboutGiveOutMerchPacksForEmployeeQuery request,
            CancellationToken cancellationToken = default)
        {
            var requests =
                await _merchRequestRepository.GetByEmployeeIdAsync(request.EmployeeId, cancellationToken);
            return (List<MerchItem>) requests[0].Items;
        }
    }
}