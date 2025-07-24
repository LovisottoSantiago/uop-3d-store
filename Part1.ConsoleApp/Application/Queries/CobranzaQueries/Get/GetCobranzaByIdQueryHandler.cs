using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Queries.CobranzaQueries.Get
{
    public class GetCobranzaByIdQueryHandler : IRequestHandler<GetCobranzaByIdQuery, Cobranza>
    {
        private readonly AppDbContext _context;
        public GetCobranzaByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cobranza> Handle(GetCobranzaByIdQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de consulta por Id
            return null;
        }
    }
} 