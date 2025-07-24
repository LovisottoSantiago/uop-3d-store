using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.EstadoQueries.Get
{
    public class GetEstadoByIdQuery : IRequest<Estado>
    {
        public int Id { get; set; }
        public GetEstadoByIdQuery(int id)
        {
            Id = id;
        }
    }
} 