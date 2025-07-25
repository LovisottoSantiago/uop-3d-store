using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Cobrar
{
    public class CobrarCommand : IRequest<Cobranza> 
    {
        public int? CobranzaId { get; set; } 
        public int OrdenDeCompraId { get; set; }
        public DateTime FechaPago { get; set; }
        public int EstadoId { get; set; }
    }
} 