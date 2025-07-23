using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Create
{
    public class CreateTipoMaterialCommand : IRequest<TipoMaterial>
    {
        public string Nombre { get; set; }
    }
} 