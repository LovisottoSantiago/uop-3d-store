using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Update
{
    public class UpdateCobranzaCommandHandler : IRequestHandler<UpdateCobranzaCommand, Cobranza>
    {
        private readonly AppDbContext _context;
        public UpdateCobranzaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cobranza> Handle(UpdateCobranzaCommand request, CancellationToken cancellationToken)
        {
            var cobranza = _context.Cobranzas.FirstOrDefault(c => c.Id ==  request.Id);

            if (cobranza == null)
            {
                return default;
            }

            cobranza.Id = request.Id;
            cobranza.OrdenDeCompraId = request.OrdenDeCompraId;
            cobranza.FechaPago = request.FechaPago;
            cobranza.MontoPagado = request.MontoPagado;
            cobranza.EstadoId = request.EstadoId;

            await _context.SaveChangesAsync();
            return cobranza;
        }
    }
} 