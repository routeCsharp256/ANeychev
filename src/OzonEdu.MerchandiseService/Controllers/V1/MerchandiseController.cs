using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.MerchandiseService.Infrastructure.Commands.CreateMerchRequest;
using OzonEdu.MerchandiseService.Infrastructure.Queries.EmployeeAggregate;
using OzonEdu.MerchandiseService.Services.Interfaces;

namespace OzonEdu.MerchandiseService.Controllers.V1
{
    /// <summary>
    ///     Сервис учёта выдачи мерча сотрудникам
    /// </summary>
    [ApiController]
    [Route("v1/merchandises/[controller]/[action]/{employeeId:long}")]
    [Produces("application/json")]
    public sealed class MerchandiseController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="mediator"></param>
        public MerchandiseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///     Запрос на выдачу мерча
        /// </summary>
        /// <param name="employeeId">идентификатор работника</param>
        /// <param name="merchPackType">идентификатор набора мерча</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{merchPackType}")]
        public async Task<ActionResult<int>> GetMerchPack(long employeeId, MerchType merchPackType,
            CancellationToken cancellationToken)
        {
            var query = new CreateMerchRequestCommand
            {
                EmployeeId = employeeId,
                MerchPackType = merchPackType
            };

            var result = await _mediator.Send(query, cancellationToken);

            return result;
        }

        /// <summary>
        ///     Возвращает информацию о выдаче мерча
        /// </summary>
        /// <param name="employeeId">идентификатор работника</param>
        /// <param name="cancellationToken">токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<MerchPack>>> GetInfoAboutMerchPacks(long employeeId,
            CancellationToken cancellationToken)
        {
            var getInfoAboutGiveOutMerchPacksForEmployeeQuery = new GetInfoAboutGiveOutMerchPacksForEmployeeQuery
            {
                EmployeeId = employeeId
            };
            var result = await _mediator.Send(getInfoAboutGiveOutMerchPacksForEmployeeQuery, cancellationToken);

            return Ok(result);
        }
    }
}