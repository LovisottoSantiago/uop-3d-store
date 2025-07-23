using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Create
{
    public class CreateDistribuidorCommand : IRequest<Distribuidor>
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
    }
} 