using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.MarcaCommands.Create;
using Part1.ConsoleApp.Application.Commands.MarcaCommands.Delete;
using Part1.ConsoleApp.Application.Commands.MarcaCommands.Update;
using Part1.ConsoleApp.Application.Queries.MarcaQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MarcaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/marca
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllMarcasQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/marca/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetMarcaByIdQuery() { Id = id };
            var marca = await _mediator.Send(query);

            if (marca == null)
                return NotFound();

            return Ok(marca);
        }

        // POST api/marca
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMarcaCommand createMarcaCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(createMarcaCommand);

            return CreatedAtAction(nameof(GetByIdAsync),
                new { id = result.Id }, result);
        }


        // PUT api/marca/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateMarcaCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/marca/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteMarcaCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }


    }
}
