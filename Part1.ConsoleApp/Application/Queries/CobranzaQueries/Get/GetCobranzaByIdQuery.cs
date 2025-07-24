using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.CobranzaQueries.Get
{
    public class GetCobranzaByIdQuery : IRequest<Cobranza>
    {
        public int Id { get; set; }
    }
} 