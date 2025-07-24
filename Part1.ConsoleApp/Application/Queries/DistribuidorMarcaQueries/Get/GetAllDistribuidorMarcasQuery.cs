using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System.Collections.Generic;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorMarcaQueries.Get
{
    public class GetAllDistribuidorMarcasQuery : IRequest<List<DistribuidorMarca>>
    {
    }
} 