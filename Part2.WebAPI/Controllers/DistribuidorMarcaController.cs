using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Create;
using Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Delete;
using Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Update;
using Part1.ConsoleApp.Application.Queries.DistribuidorMarcaQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistribuidorMarcaController : ControllerBase
    {        
        private readonly IMediator _mediator;

        public DistribuidorMarcaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/distribuidorMarca
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllDistribuidorMarcasQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/distribuidorMarca/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetDistribuidorMarcaByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/distribuidorMarca
        [HttpPost]
        public async Task <IActionResult> CreateAsync([FromBody] CreateDistribuidorMarcaCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }


        // PUT api/distribuidorMarca/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDistribuidorMarcaCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/distribuidorMarca/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteDistribuidorMarcaCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }


    }
}
