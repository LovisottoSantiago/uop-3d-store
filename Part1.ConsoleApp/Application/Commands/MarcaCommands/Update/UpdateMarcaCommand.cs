using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.MarcaCommands.Update
{
    public class UpdateMarcaCommand : IRequest<Marca>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<int> DistribuidorIds { get; set; }
    }
} 