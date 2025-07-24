using MediatR;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Delete
{
    public class DeleteDistribuidorMarcaCommandHandler : IRequestHandler<DeleteDistribuidorMarcaCommand, bool>
    {
        private readonly AppDbContext _context;
        public DeleteDistribuidorMarcaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteDistribuidorMarcaCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de borrado
            return false;
        }
    }
} 