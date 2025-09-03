using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.EstadoCommands.Create;
using Part1.ConsoleApp.Application.Commands.EstadoCommands.Delete;
using Part1.ConsoleApp.Application.Commands.EstadoCommands.Update;
using Part1.ConsoleApp.Application.Queries.EstadoQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstadoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/estado
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllEstadosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/estado/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetEstadoByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/estado
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEstadoCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }


        // PUT api/estado/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateEstadoCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/estado/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteEstadoCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }


    }
}
