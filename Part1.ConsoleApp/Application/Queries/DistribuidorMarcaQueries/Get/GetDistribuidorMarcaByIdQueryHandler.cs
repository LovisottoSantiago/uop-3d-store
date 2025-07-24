using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return await _context.DistribuidorMarcas.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
} 