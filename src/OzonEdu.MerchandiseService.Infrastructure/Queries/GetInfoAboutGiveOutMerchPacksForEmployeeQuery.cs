using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Queries
{
    /// <summary>
    /// Получить список набор мерча, которые выдавались сотруднику
    /// </summary>
    public sealed class GetInfoAboutGiveOutMerchPacksForEmployeeQuery : IRequest<List<MerchRequestItem>>
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; set; }
    }
}