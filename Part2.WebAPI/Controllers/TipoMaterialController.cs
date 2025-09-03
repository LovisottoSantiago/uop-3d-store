using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Create;
using Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Delete;
using Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Update;
using Part1.ConsoleApp.Application.Queries.TipoMaterialQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TipoMaterialController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        // GET api/tipoMaterial
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllTipoMaterialesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/tipoMaterial/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetTipoMaterialByIdQuery { Id = id };
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/tipoMaterial
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTipoMaterialCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        // PUT api/tipoMaterial/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTipoMaterialCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/tipoMaterial/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteTipoMaterialCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
