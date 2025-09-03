using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Update
{
    public class UpdateOrdenDeCompraCommandHandler : IRequestHandler<UpdateOrdenDeCompraCommand, OrdenDeCompra>
    {
        private readonly AppDbContext _context;
        public UpdateOrdenDeCompraCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompra> Handle(UpdateOrdenDeCompraCommand request, CancellationToken cancellationToken)
        {
            var ordenDeCompra = _context.OrdenDeCompras.FirstOrDefault(o => o.Id ==  request.Id);

            if (ordenDeCompra == null)
            {
                return default;
            }

            ordenDeCompra.Fecha = request.Fecha;
            ordenDeCompra.EstadoId = request.EstadoId;
            ordenDeCompra.Detalles = request.Detalles;
            ordenDeCompra.NombreCliente = request.NombreCliente;
            ordenDeCompra.NumeroCliente = request.NumeroCliente;

            await _context.SaveChangesAsync();
            return ordenDeCompra;
        }
    }
} 