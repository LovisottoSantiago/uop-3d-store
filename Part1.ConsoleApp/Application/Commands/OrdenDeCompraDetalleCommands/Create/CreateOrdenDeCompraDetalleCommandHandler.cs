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
            var ordenDeCompraDetalle = new OrdenDeCompraDetalle
            {
                OrdenDeCompraId = request.OrdenDeCompraId,
                ProductoId = request.ProductoId,
                Cantidad = request.Cantidad,
                PrecioUnitario = request.PrecioUnitario
            };

            _context.Add(ordenDeCompraDetalle);
            await _context.SaveChangesAsync();
            return ordenDeCompraDetalle;
        }
    }
} 