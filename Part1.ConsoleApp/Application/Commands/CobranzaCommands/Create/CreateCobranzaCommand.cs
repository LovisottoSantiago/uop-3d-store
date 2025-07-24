using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Create
{
    public class CreateCobranzaCommand : IRequest<Cobranza>
    {
        public int OrdenDeCompraId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public int EstadoId { get; set; }
    }
} 