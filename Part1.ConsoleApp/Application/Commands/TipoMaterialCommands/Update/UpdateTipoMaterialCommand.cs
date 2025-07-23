using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Update
{
    public class UpdateTipoMaterialCommand : IRequest<TipoMaterial>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
} 