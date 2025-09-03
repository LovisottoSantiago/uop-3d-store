using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Create;
using Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Delete;
using Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Update;
using Part1.ConsoleApp.Application.Queries.DistribuidorQueries.Get;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistribuidorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DistribuidorController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        // GET api/distribuidor
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllDistribuidoresQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/distribuidor/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetDistribuidorByIdQuery() { Id = id };
            var distribuidor = await _mediator.Send(query);

            if (distribuidor == null)
                return NotFound();

            return Ok(distribuidor);
        }

        // POST api/distribuidor
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDistribuidorCommand createDistribuidorCommand)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(createDistribuidorCommand);

            return CreatedAtAction(nameof(GetByIdAsync),
                new { id = result.Id }, result);
        }



        // PUT api/distribuidor/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDistribuidorCommand command)
        {
            if (id != command.Id)
                return BadRequest("El ID de la URL no coincide con el del body");

            var result = await _mediator.Send(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/distribuidor/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteDistribuidorCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }




    }
}
