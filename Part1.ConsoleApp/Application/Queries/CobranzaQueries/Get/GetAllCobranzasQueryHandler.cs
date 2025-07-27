using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Cobranzas
                .Include(c => c.Estado)
                .Include(c => c.OrdenDeCompra)
                .ToListAsync(cancellationToken);
        }
    }
} 