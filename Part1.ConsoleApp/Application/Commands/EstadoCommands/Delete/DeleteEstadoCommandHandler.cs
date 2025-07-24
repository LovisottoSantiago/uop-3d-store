using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Commands.EstadoCommands.Delete
{
    public class DeleteEstadoCommandHandler : IRequestHandler<DeleteEstadoCommand, Estado>
    {
        private readonly AppDbContext _context;
        public DeleteEstadoCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Estado> Handle(DeleteEstadoCommand request, CancellationToken cancellationToken)
        {
            var estado = await _context.Estados.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (estado == null)
            {
                return default;
            }

            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync(cancellationToken);
            return estado;
        }
    }
} 