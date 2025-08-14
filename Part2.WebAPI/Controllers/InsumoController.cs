using MediatR;
using Microsoft.AspNetCore.Mvc;
using Part1.ConsoleApp.Application.Commands.FilamentoCommands.Delete;
using Part1.ConsoleApp.Application.Commands.InsumoCommands.Create;
using Part1.ConsoleApp.Application.Commands.InsumoCommands.Delete;
using Part1.ConsoleApp.Application.Commands.InsumoCommands.Update;
using Part1.ConsoleApp.Application.Queries.InsumoQueries.Get;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Part2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsumoController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public InsumoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        
        // GET: api/<InsumoController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllInsumosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/<InsumoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var query = new GetInsumoByIdQuery() { Id = id};
            var insumo = await _mediator.Send(query);

            if (insumo == null)            
                return NotFound();
            
            return Ok(insumo);
        }

        // POST api/<InsumoController>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateInsumoCommand createInsumoCommand)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(createInsumoCommand);

            return CreatedAtAction(nameof(GetByIdAsync),
                new { Id = result.Id }, result);
        }

        // PUT api/<InsumoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]UpdateInsumoCommand updateInsumoCommand)
        {
            if (id != updateInsumoCommand.Id)
                return BadRequest("El ID de la URL no coincide con el del body");
            
            var result = await _mediator.Send(updateInsumoCommand);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // DELETE api/<InsumoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var command = new DeleteInsumoCommand { Id = id };
            var deleted = await _mediator.Send(command);

            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
