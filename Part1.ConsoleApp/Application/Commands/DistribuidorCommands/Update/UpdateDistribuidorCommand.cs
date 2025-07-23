using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Update
{
    public class UpdateDistribuidorCommand : IRequest<Distribuidor>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public long Telefono { get; set; }
        public string Direccion { get; set; }
    }
} 