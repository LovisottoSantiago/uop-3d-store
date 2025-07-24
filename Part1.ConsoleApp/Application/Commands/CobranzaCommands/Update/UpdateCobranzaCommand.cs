using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Update
{
    public class UpdateCobranzaCommand : IRequest<Cobranza>
    {
        public int Id { get; set; }
        public int OrdenDeCompraId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public int EstadoId { get; set; }
    }
} 