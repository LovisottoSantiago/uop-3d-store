using MediatR;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Delete
{
    public class DeleteCobranzaCommandHandler : IRequestHandler<DeleteCobranzaCommand, bool>
    {
        private readonly AppDbContext _context;
        public DeleteCobranzaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteCobranzaCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de borrado
            return false;
        }
    }
} 