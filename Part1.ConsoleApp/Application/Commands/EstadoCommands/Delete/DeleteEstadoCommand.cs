using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.EstadoCommands.Delete
{
    public class DeleteEstadoCommand : IRequest<Estado>
    {
        public int Id { get; set; }
    }
} 