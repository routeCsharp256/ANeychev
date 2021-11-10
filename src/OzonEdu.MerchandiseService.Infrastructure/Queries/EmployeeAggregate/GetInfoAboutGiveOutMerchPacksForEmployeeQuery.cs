using System.Collections.Generic;
using MediatR;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;

namespace OzonEdu.MerchandiseService.Infrastructure.Queries.EmployeeAggregate
{
    /// <summary>
    /// Получить список набор мерча, которые выдавались сотруднику
    /// </summary>
    public class GetInfoAboutGiveOutMerchPacksForEmployeeQuery : IRequest<List<MerchItem>>
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long EmployeeId { get; set; }
    }
}