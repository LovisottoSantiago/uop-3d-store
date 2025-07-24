using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.EstadoCommands.Create
{
    public class CreateEstadoCommandHandler : IRequestHandler<CreateEstadoCommand, Estado>
    {
        private readonly AppDbContext _context;
        public CreateEstadoCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Estado> Handle(CreateEstadoCommand request, CancellationToken cancellationToken)
        {
            var estado = new Estado 
            { 
                NombreEstado = request.NombreEstado 
            };

            _context.Estados.Add(estado);
            await _context.SaveChangesAsync(cancellationToken);
            return estado;
        }
    }
} 