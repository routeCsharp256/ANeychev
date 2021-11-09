using CSharpCourse.Core.Lib.Enums;
using MediatR;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public class CreateMerchRequestCommand : IRequest<int>
    {
        public long EmployeeId { get; set; }
        public MerchType MerchPackType { get; set; }
    }
}