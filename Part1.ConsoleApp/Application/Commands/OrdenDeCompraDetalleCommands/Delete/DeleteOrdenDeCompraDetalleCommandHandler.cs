using MediatR;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Delete
{
    public class DeleteOrdenDeCompraDetalleCommandHandler : IRequestHandler<DeleteOrdenDeCompraDetalleCommand, bool>
    {
        private readonly AppDbContext _context;
        public DeleteOrdenDeCompraDetalleCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteOrdenDeCompraDetalleCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de borrado
            return false;
        }
    }
} 