using DevsuTest.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DevsuTest.Controllers
{
    public class ReportesController : BaseApiController
    {
        private readonly IReportesService _reportesService;

        public ReportesController(ILogger<ReportesController> logger, IReportesService reportesService)
            : base(logger)
        {
            _reportesService = reportesService;
        }

        /// <summary>
        /// Retrieves the the list of movements for a specified client within a given date range.
        /// </summary>
        /// <param name="clienteId">ID of the client for which the report is being generated. 
        /// It is a required parameter and must be specified in the request.</param>
        /// <param name="fechaDesde">Starting date of the report. Optional parameter.</param>
        /// <param name="fechaHasta">End date of  the report. Optional parameter.</param>
        /// <returns>
        /// 200 (Ok) List with all movements for the specified client in the given date range.
        /// 400 (BadRequest) If no clientId is provided
        /// </returns>
        [HttpGet("listado-movimientos")]
        public async Task<IActionResult> GetListadoMovimientosCliente([FromQuery] int clienteId, [FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            if (clienteId == default)
            {
                return BadRequest(JsonSerializer.Serialize(new { PropertyName = "ClienteId", Error = "Debe especificiar el cliente para obtener el reporte" }));
            }
            return Ok(await _reportesService.GetListadoMovimientos(clienteId, fechaDesde, fechaHasta));
        }

        /// <summary>
        /// Retrieves the estado de cuenta (account statement) for each account of the specified client within a given date range.
        /// </summary>
        /// <param name="clienteId">ID of the client for which the report is being generated. 
        /// It is a required parameter and must be specified in the request.</param>
        /// <param name="fechaDesde">Starting date of the report. Optional parameter.</param>
        /// <param name="fechaHasta">End date of  the report. Optional parameter.</param>
        /// <returns>
        /// 200 (Ok) List with all accounts for the specified client with their respective balance in the given date range.
        /// 400 (BadRequest) If no clientId is provided
        /// </returns>
        [HttpGet("estado-cuenta")]
        public async Task<IActionResult> GetEstadoCuentasCliente([FromQuery] int clienteId, [FromQuery] DateTime? fechaDesde, [FromQuery] DateTime? fechaHasta)
        {
            if (clienteId == default)
            {
                return BadRequest(JsonSerializer.Serialize(new { PropertyName = "ClienteId", Error = "Debe especificiar el cliente para obtener el reporte" }));
            }
            return Ok(await _reportesService.GetEstadoCuenta(clienteId, fechaDesde, fechaHasta));
        }
    }
}
