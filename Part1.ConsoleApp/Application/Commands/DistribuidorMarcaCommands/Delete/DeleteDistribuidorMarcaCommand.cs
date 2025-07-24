using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Delete
{
    public class DeleteDistribuidorMarcaCommand : IRequest<DistribuidorMarca>
    {
        public int Id { get; set; }
    }
} 