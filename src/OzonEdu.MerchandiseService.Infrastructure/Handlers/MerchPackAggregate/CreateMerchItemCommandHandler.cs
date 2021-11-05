using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchItem;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchPackAggregate
{
    public class CreateMerchItemCommandHandler : IRequestHandler<CreateMerchItemCommand, int>
    {

        public async Task<int> Handle(CreateMerchItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}