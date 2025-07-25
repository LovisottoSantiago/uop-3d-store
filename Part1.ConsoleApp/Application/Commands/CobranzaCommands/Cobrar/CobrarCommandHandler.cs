using MediatR;
using Microsoft.EntityFrameworkCore;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Cobrar
{
    public class CobrarCommandHandler : IRequestHandler<CobrarCommand, Cobranza>
    {
        private readonly AppDbContext _context;
        public CobrarCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cobranza> Handle(CobrarCommand request, CancellationToken cancellationToken)
        {
            Cobranza? cobranza;
            if (request.CobranzaId.HasValue)
            {
                cobranza = await _context.Cobranzas.Include(c => c.OrdenDeCompra).ThenInclude(o => o.Detalles).ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(c => c.Id == request.CobranzaId.Value, cancellationToken);
                if (cobranza == null)
                    return default;

                cobranza.OrdenDeCompraId = request.OrdenDeCompraId;
                cobranza.FechaPago = request.FechaPago;
                // No actualizamos MontoPagado 
                cobranza.EstadoId = request.EstadoId;
            }
            else
            {
                cobranza = new Cobranza
                {
                    OrdenDeCompraId = request.OrdenDeCompraId,
                    FechaPago = request.FechaPago,
                    EstadoId = request.EstadoId
                };

                // Calcular el monto pagado automáticamente
                cobranza.OrdenDeCompra = await _context.OrdenDeCompras.Include(o => o.Detalles).ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(o => o.Id == request.OrdenDeCompraId, cancellationToken);
                if (cobranza.OrdenDeCompra != null)
                {
                    cobranza.MontoPagado = cobranza.OrdenDeCompra.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
                }
                _context.Cobranzas.Add(cobranza);
            }

            var estado = await _context.Estados.FindAsync(new object[] { request.EstadoId }, cancellationToken);
            if (estado != null && estado.NombreEstado.ToLower() == "pagado")
            {
                var orden = cobranza.OrdenDeCompra ?? await _context.OrdenDeCompras.Include(o => o.Detalles).ThenInclude(d => d.Producto)
                    .FirstOrDefaultAsync(o => o.Id == cobranza.OrdenDeCompraId, cancellationToken);
                if (orden != null)
                {
                    foreach (var detalle in orden.Detalles)
                    {
                        if (detalle.Producto != null)
                        {
                            detalle.Producto.Stock -= detalle.Cantidad;
                        }
                    }
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
            return cobranza;
        }
    }
} 