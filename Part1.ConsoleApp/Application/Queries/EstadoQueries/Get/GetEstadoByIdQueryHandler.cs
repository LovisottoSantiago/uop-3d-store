using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.EstadoQueries.Get
{
    public class GetEstadoByIdQueryHandler : IRequestHandler<GetEstadoByIdQuery, Estado>
    {
        private readonly AppDbContext _context;
        public GetEstadoByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Estado> Handle(GetEstadoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Estados.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
} 