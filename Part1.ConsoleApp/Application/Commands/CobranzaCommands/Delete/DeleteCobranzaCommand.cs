using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Delete
{
    public class DeleteCobranzaCommand : IRequest<Cobranza>
    {
        public int Id { get; set; }
    }
} 