using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Delete
{
    public class DeleteTipoMaterialCommand : IRequest<TipoMaterial>
    {
        public int Id { get; set; }
    }
} 