using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraDetalleCommands.Delete
{
    public class DeleteOrdenDeCompraDetalleCommand : IRequest<OrdenDeCompraDetalle>
    {
        public int Id { get; set; }
    }
} 