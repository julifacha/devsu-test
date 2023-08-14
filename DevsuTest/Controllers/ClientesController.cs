using DevsuTest.Application.DTO;
using DevsuTest.Application.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DevsuTest.Controllers
{
    public class ClientesController : BaseApiController
    {
        private readonly IClientesService _clientesService;

        public ClientesController(ILogger<ClientesController> logger, IClientesService clientesService)
            : base(logger)
        {
            _clientesService = clientesService;
        }

        /// <summary>
        /// Returns all Clients
        /// </summary>
        /// <returns>
        /// A List of clients
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _clientesService.GetAll());
        }

        /// <summary>
        /// Retrieves a client by their ID and returns it as a response.
        /// </summary>
        /// <param name="id">The client Primary Key Id.</param>
        /// <returns>
        /// 200 (OK) if found
        /// 404 (Not Found) if not found
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            ClienteDto cliente = await _clientesService.GetById(id);
            return Ok(cliente);
        }

        /// <summary>
        /// Cretes a new Client resource
        /// </summary>
        /// <param name="ClienteDto">The ClientDto body</param>
        /// <returns>
        /// 201 (Created) with the created Entity as a body
        /// 400 (Bad Request) if Validation Errors occur
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDto clienteDto)
        {
            ClienteDto cliente = await _clientesService.Create(clienteDto);
            return Created(string.Empty, cliente);
        }

        /// <summary>
        /// Updates a client with the given ID using the provided client data.
        /// </summary>
        /// <param name="id">The Client Id.</param>
        /// <param name="ClienteDto">The ClientDto body.</param>
        /// <returns>
        /// 200 (Ok) with the updated Entity as a body
        /// 400 (Bad Request) if Validation Errors occur
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ClienteDto clienteDto)
        {
            ClienteDto cliente = await _clientesService.Update(id, clienteDto);
            return Created(string.Empty, cliente);
        }

        /// <summary>
        /// Applies partial updates to the specified resource
        /// </summary>
        /// <param name="id">The Client Id.</param>
        /// <param name="patchDoc">The Json patch document with the fields to update.</param>
        /// <returns>
        /// 200 (Ok) with the updated Entity as a body
        /// 400 (Bad Request) if Validation Errors occur
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<ClienteDto> patchDoc)
        {
            ClienteDto cliente = await _clientesService.Patch(id, patchDoc);
            return Ok(cliente);
        }

        /// <summary>
        /// Hard deletes the specified client resource.
        /// </summary>
        /// <param name="id">The Client Id.</param>
        /// <returns>
        /// 204 (No Content) if succesful
        /// 404 (Not Found) if no matching entity is found.
        /// 500 (Internal Server Error) if the entity has related entities and can not be deleted
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _clientesService.Delete(id);
            return NoContent();
        }
    }
}
