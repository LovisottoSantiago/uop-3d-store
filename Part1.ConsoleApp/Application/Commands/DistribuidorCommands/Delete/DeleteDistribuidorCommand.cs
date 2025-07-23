using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Delete
{
    public class DeleteDistribuidorCommand : IRequest<Distribuidor>
    {
        public int Id { get; set; }
    }
} 