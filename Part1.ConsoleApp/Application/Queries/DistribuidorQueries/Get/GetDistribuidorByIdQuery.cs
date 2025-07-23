using MediatR;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorQueries.Get
{
    public class GetDistribuidorByIdQuery : IRequest<Distribuidor>
    {
        public int Id { get; set; }
    }
} 