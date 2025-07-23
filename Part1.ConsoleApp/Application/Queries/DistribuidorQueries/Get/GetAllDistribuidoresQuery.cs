using MediatR;
using System.Collections.Generic;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorQueries.Get
{
    public class GetAllDistribuidoresQuery : IRequest<IEnumerable<Distribuidor>>
    {
    }
} 