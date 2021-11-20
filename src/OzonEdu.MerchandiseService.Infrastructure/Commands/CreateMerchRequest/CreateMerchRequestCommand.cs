using MediatR;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public sealed class CreateMerchRequestCommand : IRequest
    {
        public long EmployeeId { get; set; }
        
        public string Email { get; set; }
        public int MerchPackTypeId { get; set; }
    }
}