using MediatR;
using System.Collections.Generic;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.MarcaQueries.Get
{
    public class GetAllMarcasQuery : IRequest<IEnumerable<Marca>>
    {
    }
} 