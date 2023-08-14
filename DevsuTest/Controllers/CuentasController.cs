using DevsuTest.Application.DTO;
using DevsuTest.Application.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DevsuTest.Controllers
{
    public class CuentasController : BaseApiController
    {
        private readonly ICuentasService _cuentasService;

        public CuentasController(ILogger<CuentasController> logger, ICuentasService cuentasService)
            : base(logger)
        {
            _cuentasService = cuentasService;
        }

        /// <summary>
        /// Returns all Accounts
        /// </summary>
        /// <returns>
        /// A List of accounts
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _cuentasService.GetAll());
        }

        /// <summary>
        /// Retrieves an account by their Id and returns it as a response.
        /// </summary>
        /// <param name="id">The account Primary Key Id.</param>
        /// <returns>
        /// 200 (OK) if found
        /// 404 (Not Found) if not found
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            CuentaDto cuenta = await _cuentasService.GetById(id);
            return Ok(cuenta);
        }

        /// <summary>
        /// Cretes a new Account resource
        /// </summary>
        /// <param name="CuentaDto">The CuentaDto body</param>
        /// <returns>
        /// 201 (Created) with the created Entity as a body
        /// 400 (Bad Request) if Validation Errors occur
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CuentaDto cuentaDto)
        {
            CuentaDto cuenta = await _cuentasService.Create(cuentaDto);
            return Created(string.Empty, cuenta);
        }

        /// <summary>
        /// Updates an account with the given ID using the provided account data.
        /// </summary>
        /// <param name="id">The account Id.</param>
        /// <param name="ClienteDto">The AcountDto body.</param>
        /// <returns>
        /// 200 (Ok) with the updated Entity as a body
        /// 400 (Bad Request) if Validation Errors occur
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CuentaDto cuentaDto)
        {
            CuentaDto cuenta = await _cuentasService.Update(id, cuentaDto);
            return Ok(cuenta);
        }

        /// <summary>
        /// Applies partial updates to the specified resource
        /// </summary>
        /// <param name="id">The account Id.</param>
        /// <param name="patchDoc">The Json patch document with the fields to update.</param>
        /// <returns>
        /// 200 (Ok) with the updated Entity as a body
        /// 400 (Bad Request) if Validation Errors occur
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<CuentaDto> patchDoc)
        {
            CuentaDto cuenta = await _cuentasService.Patch(id, patchDoc);
            return Ok(cuenta);
        }

        /// <summary>
        /// Hard deletes the specified resource.
        /// </summary>
        /// <param name="id">The account Id.</param>
        /// <returns>
        /// 204 (No Content) if succesful
        /// 404 (Not Found) if no matching entity is found.
        /// 500 (Internal Server Error) if the entity has related entities and can not be deleted
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _cuentasService.Delete(id);
            return NoContent();
        }
    }
}