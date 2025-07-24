using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Update
{
    public class UpdateOrdenDeCompraDetalleCommand : IRequest<OrdenDeCompraDetalle>
    {
        public int Id { get; set; }
        public int OrdenDeCompraId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
} 