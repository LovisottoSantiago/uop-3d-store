using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorQueries.Get
{
    public class GetDistribuidorByIdQueryHandler : IRequestHandler<GetDistribuidorByIdQuery, Distribuidor>
    {
        private readonly AppDbContext _context;
        public GetDistribuidorByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Distribuidor> Handle(GetDistribuidorByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Distribuidores.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
} 