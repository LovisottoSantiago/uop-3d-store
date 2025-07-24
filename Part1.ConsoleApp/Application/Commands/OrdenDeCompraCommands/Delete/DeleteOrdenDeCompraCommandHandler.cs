using MediatR;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Delete
{
    public class DeleteOrdenDeCompraCommandHandler : IRequestHandler<DeleteOrdenDeCompraCommand, bool>
    {
        private readonly AppDbContext _context;
        public DeleteOrdenDeCompraCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteOrdenDeCompraCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de borrado
            return false;
        }
    }
} 