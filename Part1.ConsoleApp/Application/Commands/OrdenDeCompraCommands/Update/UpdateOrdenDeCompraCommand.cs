using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Update
{
    public class UpdateOrdenDeCompraCommand : IRequest<OrdenDeCompra>
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int EstadoId { get; set; }
        public List<OrdenDeCompraDetalle> Detalles { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int NumeroCliente { get; set; }
    }
} 