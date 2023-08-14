using DevsuTest.Application.DTO;
using DevsuTest.Application.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DevsuTest.Controllers
{
    public class MovimientosController : BaseApiController
    {
        private readonly IMovimientosService _movimientosService;

        public MovimientosController(ILogger<MovimientosController> logger, IMovimientosService movimientosService)
            : base(logger)
        {
            _movimientosService = movimientosService;
        }

        /// <summary>
        /// Returns all movements
        /// </summary>
        /// <returns>
        /// A List of movements
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _movimientosService.GetAll());
        }

        /// <summary>
        /// Retrieves a movement by their Id and returns it as a response.
        /// </summary>
        /// <param name="id">The movement Primary Key Id.</param>
        /// <returns>
        /// 200 (OK) if found
        /// 404 (Not Found) if not found
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            MovimientoDto movimiento = await _movimientosService.GetById(id);
            if (movimiento == null)
            {
                return NotFound();
            }
            return Ok(movimiento);
        }

        /// <summary>
        /// Cretes a withdrawal/deposit movement in the specified account
        /// </summary>
        /// <param name="movimientoDto">The MovimientoDto body</param>
        /// <returns>
        /// 201 (Created) with the created Entity as a body
        /// 400 (Bad Request) if Validation Errors occur
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MovimientoDto movimientoDto)
        {
            MovimientoDto movimiento = await _movimientosService.Create(movimientoDto);
            return Created(string.Empty, movimiento);
        }

        /// <summary>
        /// Hard deletes the specified resource.
        /// </summary>
        /// <param name="id">The account Id.</param>
        /// <returns>
        /// 204 (No Content) if succesful
        /// 404 (Not Found) if no matching entity is found.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _movimientosService.Delete(id);
            return NoContent();
        }
    }
}
