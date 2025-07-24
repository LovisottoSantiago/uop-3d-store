using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System.Collections.Generic;

namespace Part1.ConsoleApp.Application.Queries.CobranzaQueries.Get
{
    public class GetAllCobranzasQuery : IRequest<List<Cobranza>>
    {
    }
} 