using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Commands.EstadoCommands.Update
{
    public class UpdateEstadoCommandHandler : IRequestHandler<UpdateEstadoCommand, Estado>
    {
        private readonly AppDbContext _context;
        public UpdateEstadoCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Estado> Handle(UpdateEstadoCommand request, CancellationToken cancellationToken)
        {
            var estado = await _context.Estados.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (estado == null)
            {
                return default;
            }
            estado.NombreEstado = request.NombreEstado;
            await _context.SaveChangesAsync(cancellationToken);
            return estado;
        }
    }
} 