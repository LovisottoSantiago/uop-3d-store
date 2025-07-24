using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.EstadoCommands.Create
{
    public class CreateEstadoCommand : IRequest<Estado>
    {
        public string NombreEstado { get; set; }
    }
} 