using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.FilamentoCommands.Create;
using Part1.ConsoleApp.Application.Commands.FilamentoCommands.Delete;
using Part1.ConsoleApp.Application.Commands.FilamentoCommands.Update;
using Part1.ConsoleApp.Application.Queries.FilamentoQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilamentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilamentoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        // GET api/filamento
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllFilamentosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/filamento/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetFilamentoByIdQuery { Id = id };
            var filamento = await _mediator.Send(query);

            if (filamento == null)
                return NotFound();

            return Ok(filamento);
        }

        // POST api/filamento
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateFilamentoCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        // PUT api/filamento/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateFilamentoCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/filamento/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteFilamentoCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }



    }
}
