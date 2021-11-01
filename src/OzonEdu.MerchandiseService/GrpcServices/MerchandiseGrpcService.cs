using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Grpc;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.GrpcServices
{
    public sealed class MerchandiseGrpcService : MerchandiseGrpc.MerchandiseGrpcBase
    {
        private readonly IMerchService _merchService;

        public MerchandiseGrpcService(IMerchService merchService)
        {
            _merchService = merchService;
        }

        public override async Task<GetInfoResponse> GetInfo(GetInfoRequest request, ServerCallContext context)
        {
            // context.CancellationToken

            var result = await _merchService.GetInfoAsync(request.EmployeeId, context.CancellationToken);


            return new GetInfoResponse()
            {
                Items =
                {
                    result.Select(x => new MerchItemResponseUnit
                    {
                        ItemId = x.ItemId,
                        ItemName = x.ItemName,
                        Quantity = x.Quantity
                    })
                }
            };
        }

        public override async Task<GiveOutResponse> GiveOut(GiveOutRequest request, ServerCallContext context)
        {
            await _merchService.GiveOutAsync(request.EmployeeId, context.CancellationToken);
            return new GiveOutResponse();
        }
    }
}