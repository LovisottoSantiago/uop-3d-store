using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Create;
using Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Delete;
using Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Update;
using Part1.ConsoleApp.Application.Queries.OrdenDeCompraQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenDeCompraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdenDeCompraController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/ordenDeCompra
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllOrdenesDeCompraQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/ordenDeCompra/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetOrdenDeCompraByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/ordenDeCompra
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrdenDeCompraCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }


        // PUT api/ordenDeCompra/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateOrdenDeCompraCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/ordenDeCompra/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteOrdenDeCompraCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }


    }
}
