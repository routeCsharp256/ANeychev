using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;

namespace OzonEdu.MerchandiseService.Infrastructure.Handlers.MerchPackAggregate
{
    public sealed class CreateMerchRequestCommandHandler : IRequestHandler<CreateMerchRequestCommand, int>
    {
        /// <summary>
        ///     Запрос на выдачу сотруднику набора мерча
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Возвращает статус запроса</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> Handle(CreateMerchRequestCommand request, CancellationToken cancellationToken =default)
        {
            throw new NotImplementedException();
        }
    }
}