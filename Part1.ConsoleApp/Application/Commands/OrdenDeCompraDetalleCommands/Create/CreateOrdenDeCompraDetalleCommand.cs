using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Create
{
    public class CreateOrdenDeCompraDetalleCommand : IRequest<OrdenDeCompraDetalle>
    {
        public int OrdenDeCompraId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
} 