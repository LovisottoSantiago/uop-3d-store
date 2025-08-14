using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.CobranzaCommands.Create;
using Part1.ConsoleApp.Application.Commands.CobranzaCommands.Delete;
using Part1.ConsoleApp.Application.Commands.CobranzaCommands.Update;
using Part1.ConsoleApp.Application.Queries.CobranzaQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobranzaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CobranzaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        // GET api/cobranza
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCobranzasQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        // GET api/cobranza/{id]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new  GetCobranzaByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


        // POST api/cobranza
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCobranzaCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        // PUT api/cobranza/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateCobranzaCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/cobranza/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteCobranzaCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }

    }

}
