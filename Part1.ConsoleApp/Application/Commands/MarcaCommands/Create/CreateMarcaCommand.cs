using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.MarcaCommands.Create
{
    public class CreateMarcaCommand : IRequest<Marca>
    {
        public string Nombre { get; set; }
        public int DistribuidorId { get; set; } 
    }
} 