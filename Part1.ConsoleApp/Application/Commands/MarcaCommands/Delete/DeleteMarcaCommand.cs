using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.MarcaCommands.Delete
{
    public class DeleteMarcaCommand : IRequest<Marca>
    {
        public int Id { get; set; }
    }
} 