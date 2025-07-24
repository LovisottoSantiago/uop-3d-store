using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.EstadoCommands.Update
{
    public class UpdateEstadoCommand : IRequest<Estado>
    {
        public int Id { get; set; }
        public string NombreEstado { get; set; }
    }
} 