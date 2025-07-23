using MediatR;
using System.Collections.Generic;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.TipoMaterialQueries.Get
{
    public class GetAllTipoMaterialesQuery : IRequest<IEnumerable<TipoMaterial>>
    {
    }
} 