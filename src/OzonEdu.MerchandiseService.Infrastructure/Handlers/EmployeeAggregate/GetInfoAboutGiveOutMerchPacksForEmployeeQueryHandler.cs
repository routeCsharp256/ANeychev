using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Queries.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.EmployeeAggregate
{
    public class GetInfoAboutGiveOutMerchPacksForEmployeeQueryHandler : IRequestHandler<GetInfoAboutGiveOutMerchPacksForEmployeeQuery, List<MerchPack>>
    {
        public async Task<List<MerchPack>> Handle(GetInfoAboutGiveOutMerchPacksForEmployeeQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}