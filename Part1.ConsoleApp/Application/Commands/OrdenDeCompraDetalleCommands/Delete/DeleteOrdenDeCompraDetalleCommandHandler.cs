using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Delete
{
    public class DeleteOrdenDeCompraDetalleCommandHandler : IRequestHandler<DeleteOrdenDeCompraDetalleCommand, OrdenDeCompraDetalle>
    {
        private readonly AppDbContext _context;
        public DeleteOrdenDeCompraDetalleCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompraDetalle> Handle(DeleteOrdenDeCompraDetalleCommand request, CancellationToken cancellationToken)
        {
            var ordenDeCompraDetalle = _context.OrdenDeCompraDetalles.FirstOrDefault(o => o.Id == request.Id);

            if (ordenDeCompraDetalle == null)
            {
                return default;
            }

            _context.Remove(ordenDeCompraDetalle);
            await _context.SaveChangesAsync();
            return ordenDeCompraDetalle;
        }
    }
} 