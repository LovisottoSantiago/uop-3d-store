using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Part1.ConsoleApp.Application.Queries.CobranzaQueries.Get
{
    public class GetAllCobranzasQueryHandler : IRequestHandler<GetAllCobranzasQuery, List<Cobranza>>
    {
        private readonly AppDbContext _context;
        public GetAllCobranzasQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Cobranza>> Handle(GetAllCobranzasQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de consulta
            return null;
        }
    }
} 