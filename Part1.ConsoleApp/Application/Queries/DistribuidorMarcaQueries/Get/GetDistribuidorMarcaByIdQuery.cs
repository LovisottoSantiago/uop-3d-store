using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorMarcaQueries.Get
{
    public class GetDistribuidorMarcaByIdQuery : IRequest<DistribuidorMarca>
    {
        public int Id { get; set; }
    }
} 