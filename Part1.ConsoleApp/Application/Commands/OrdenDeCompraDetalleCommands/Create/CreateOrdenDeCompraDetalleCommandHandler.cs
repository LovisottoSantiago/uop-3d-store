using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Create
{
    public class CreateOrdenDeCompraDetalleCommandHandler : IRequestHandler<CreateOrdenDeCompraDetalleCommand, OrdenDeCompraDetalle>
    {
        private readonly AppDbContext _context;
        public CreateOrdenDeCompraDetalleCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompraDetalle> Handle(CreateOrdenDeCompraDetalleCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de creación
            return null;
        }
    }
} 