using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.MarcaQueries.Get
{
    public class GetMarcaByIdQuery : IRequest<Marca>
    {
        public int Id { get; set; }
    }
} 