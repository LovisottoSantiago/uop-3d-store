using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Update
{
    public class UpdateDistribuidorMarcaCommand : IRequest<DistribuidorMarca>
    {
        public int Id { get; set; }
        public int DistribuidorId { get; set; }
        public int MarcaId { get; set; }
    }
} 