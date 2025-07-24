using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Update
{
    public class UpdateOrdenDeCompraDetalleCommandHandler : IRequestHandler<UpdateOrdenDeCompraDetalleCommand, OrdenDeCompraDetalle>
    {
        private readonly AppDbContext _context;
        public UpdateOrdenDeCompraDetalleCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompraDetalle> Handle(UpdateOrdenDeCompraDetalleCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de actualización
            return null;
        }
    }
} 