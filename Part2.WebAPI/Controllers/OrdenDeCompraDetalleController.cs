using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Create;
using Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Delete;
using Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Update;
using Part1.ConsoleApp.Application.Queries.OrdenDeCompraDetalleQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenDeCompraDetalleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdenDeCompraDetalleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/ordenDeCompraDetalle
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllOrdenesDeCompraDetalleQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/ordenDeCompraDetalle/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetOrdenDeCompraDetalleByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/ordenDeCompraDetalle
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrdenDeCompraDetalleCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }


        // PUT api/ordenDeCompraDetalle/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateOrdenDeCompraDetalleCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/ordenDeCompraDetalle/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteOrdenDeCompraDetalleCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }


    }
}
