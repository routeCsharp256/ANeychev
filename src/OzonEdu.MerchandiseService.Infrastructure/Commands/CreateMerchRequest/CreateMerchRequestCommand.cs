using MediatR;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public sealed class CreateMerchRequestCommand : IRequest
    {
        public long EmployeeId { get; set; }
        
        public string ClothingSize { get; set; }
        public string EmployeeEmail { get; set; }
        public string ManagerEmail { get; set; }
        public int MerchPackTypeId { get; set; }
    }
}