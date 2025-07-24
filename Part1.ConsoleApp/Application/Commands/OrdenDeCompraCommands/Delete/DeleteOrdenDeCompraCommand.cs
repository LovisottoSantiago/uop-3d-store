using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Delete
{
    public class DeleteOrdenDeCompraCommand : IRequest<OrdenDeCompra>
    {
        public int Id { get; set; }
    }
} 