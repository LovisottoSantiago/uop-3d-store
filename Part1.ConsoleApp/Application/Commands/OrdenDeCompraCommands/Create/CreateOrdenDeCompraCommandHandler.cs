using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Create
{
    public class CreateOrdenDeCompraCommandHandler : IRequestHandler<CreateOrdenDeCompraCommand, OrdenDeCompra>
    {
        private readonly AppDbContext _context;
        public CreateOrdenDeCompraCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompra> Handle(CreateOrdenDeCompraCommand request, CancellationToken cancellationToken)
        {
            var ordenDeCompra = new OrdenDeCompra
            {
                Fecha = request.Fecha,
                EstadoId = request.EstadoId,
                Detalles = request.Detalles.Select(d => new OrdenDeCompraDetalle
                {
                    ProductoId = d.ProductoId,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()
            };

            _context.Add(ordenDeCompra);
            await _context.SaveChangesAsync();
            return ordenDeCompra;
        }
    }
} 