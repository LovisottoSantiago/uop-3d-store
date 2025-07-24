using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Cobranzas.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
} 