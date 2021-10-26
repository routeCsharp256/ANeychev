using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Controllers.V1
{
    [ApiController]
    [Route("v1/api/merchandises/{employeeId:long}")]
    [Produces("application/json")]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMerchService _merchService;

        public MerchandiseController(IMerchService merchService) => _merchService = merchService;

        [HttpGet]
        public async Task<IActionResult> GiveOut(long employeeId, CancellationToken cancellationToken)
        {
            await _merchService.GiveOutAsync(employeeId, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<MerchItemResponse>>> GetInfo(long employeeId,
            CancellationToken cancellationToken) =>
            await _merchService.GetInfoAsync(employeeId, cancellationToken);
    }
}