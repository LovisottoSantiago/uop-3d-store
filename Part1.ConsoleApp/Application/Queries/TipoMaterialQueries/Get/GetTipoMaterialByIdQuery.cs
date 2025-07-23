using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.TipoMaterialQueries.Get
{
    public class GetTipoMaterialByIdQuery : IRequest<TipoMaterial>
    {
        public int Id { get; set; }
    }
} 