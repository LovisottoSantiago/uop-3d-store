using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorMarcaQueries.Get
{
    public class GetDistribuidorMarcaByIdQueryHandler : IRequestHandler<GetDistribuidorMarcaByIdQuery, DistribuidorMarca>
    {
        private readonly AppDbContext _context;
        public GetDistribuidorMarcaByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DistribuidorMarca> Handle(GetDistribuidorMarcaByIdQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de consulta por Id
            return null;
        }
    }
} 