using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Create
{
    public class CreateDistribuidorMarcaCommand : IRequest<DistribuidorMarca>
    {
        public int DistribuidorId { get; set; }
        public int MarcaId { get; set; }
    }
} 