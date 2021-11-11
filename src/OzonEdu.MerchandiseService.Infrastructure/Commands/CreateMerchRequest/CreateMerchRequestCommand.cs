using CSharpCourse.Core.Lib.Enums;
using MediatR;

namespace OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest
{
    public sealed class CreateMerchRequestCommand : IRequest
    {
        public long EmployeeId { get; set; }
        public int MerchPackTypeId { get; set; }
    }
}