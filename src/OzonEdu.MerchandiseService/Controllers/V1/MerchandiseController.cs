using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.HttpModels;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Controllers.V1
{
    /// <summary>
    /// Сервис учёта выдачи мерча сотрудникам
    /// </summary>
    [ApiController]
    [Route("v1/merchandises/[controller]/[action]/{employeeId:long}")]
    [Produces("application/json")]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMerchService _merchService;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="merchService"></param>
        public MerchandiseController(IMerchService merchService) => _merchService = merchService;

        /// <summary>
        /// Запрос на выдачу мерча
        /// </summary>
        /// <param name="employeeId">идентификатор работника</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GiveOut(long employeeId, CancellationToken cancellationToken)
        {
            await _merchService.GiveOutAsync(employeeId, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Возвращает информацию о выдаче мерча
        /// </summary>
        /// <param name="employeeId">идентификатор работника</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<MerchItemResponse>>> GetInfo(long employeeId,
            CancellationToken cancellationToken) =>
            await _merchService.GetInfoAsync(employeeId, cancellationToken);
    }
}