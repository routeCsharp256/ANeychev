using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Infrastructure.Workers.Interfaces;

namespace OzonEdu.MerchandiseService.Controllers.V1
{
#if DEBUG
    /// <summary>
    ///     Тестовый контроллер
    /// </summary>
    [ApiController]
    [Route("v1/merchandises/[controller]/[action]")]
    [Produces("application/json")]
    public sealed class TestMerchandiseController : ControllerBase
    {
        private readonly IMerchRequestWorker _merchRequestWorker;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="merchRequestWorker"></param>
        public TestMerchandiseController(IMerchRequestWorker merchRequestWorker)
        {
            _merchRequestWorker = merchRequestWorker;
        }

        /// <summary>
        ///     Тестовый запуск job AutoGenerateMerckRequest
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<IActionResult> StartAutoGenerateMerckRequest(CancellationToken cancellationToken = default)
        {
            await _merchRequestWorker.AutoGenerateMerckRequest(cancellationToken);
            return Ok();
        }
    }
#endif
}