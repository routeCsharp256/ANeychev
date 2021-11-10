using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using OzonEdu.MerchandiseService.Grpc;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;
using OzonEdu.MerchandiseService.Infrastructure.Queries.EmployeeAggregate;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MerchandiseGrpcService : MerchandiseGrpc.MerchandiseGrpcBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public MerchandiseGrpcService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Возвращает информацию о выдаче мерча
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GetInfoResponse> GetInfoAboutMerchPacks(GetInfoRequest request,
            ServerCallContext context)
        {
            var getInfoAboutGiveOutMerchPacksForEmployeeQuery = new GetInfoAboutGiveOutMerchPacksForEmployeeQuery
            {
                EmployeeId = request.EmployeeId
            };

            var result = await _mediator.Send(getInfoAboutGiveOutMerchPacksForEmployeeQuery, default);

            return new GetInfoResponse
            {
                Items =
                {
                    result.Select(x => new MerchItemResponseUnit
                    {
                        ItemId = x.Sku.Value,
                        ItemName = x.Name.Value,
                        Quantity = x.Quantity.Value
                    })
                }
            };
        }

        /// <summary>
        ///     Запрос на выдачу мерча
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<GiveOutResponse> GetMerchPack(GiveOutRequest request, ServerCallContext context)
        {
            var query = new CreateMerchRequestCommand
            {
                EmployeeId = request.EmployeeId,
                MerchPackTypeId = request.MerchPackTypeId
            };

            var result = await _mediator.Send(query, context.CancellationToken);

            return new GiveOutResponse
            {
                MerchRequestStatusId = result
            };
        }
    }
}