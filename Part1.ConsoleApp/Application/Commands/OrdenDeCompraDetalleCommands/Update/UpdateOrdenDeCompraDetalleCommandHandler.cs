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
            var ordenDeCompraDetalle = _context.OrdenDeCompraDetalles.FirstOrDefault(o => o.Id == request.Id);

            if (ordenDeCompraDetalle == null)
            {
                return default;
            }

            ordenDeCompraDetalle.OrdenDeCompraId = request.OrdenDeCompraId;
            ordenDeCompraDetalle.ProductoId = request.ProductoId;
            ordenDeCompraDetalle.Cantidad = request.Cantidad;
            ordenDeCompraDetalle.PrecioUnitario = request.PrecioUnitario;

            await _context.SaveChangesAsync();
            return ordenDeCompraDetalle;
        }
    }
} 