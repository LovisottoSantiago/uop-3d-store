using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorMarcaQueries.Get
{
    public class GetAllDistribuidorMarcasQueryHandler : IRequestHandler<GetAllDistribuidorMarcasQuery, List<DistribuidorMarca>>
    {
        private readonly AppDbContext _context;
        public GetAllDistribuidorMarcasQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<DistribuidorMarca>> Handle(GetAllDistribuidorMarcasQuery request, CancellationToken cancellationToken)
        {
            return await _context.DistribuidorMarcas.ToListAsync(cancellationToken);
        }
    }
} 