using MediatR;
using Microsoft.EntityFrameworkCore;
using Part1.ConsoleApp.Application.Queries.FilamentoQueries.Get;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Queries.DistribuidorQueries.Get
{
    public class GetAllDistribuidoresQueryHandler : IRequestHandler<GetAllDistribuidoresQuery, IEnumerable<Distribuidor>>
    {
        private readonly AppDbContext _context;
        public GetAllDistribuidoresQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Distribuidor>> Handle(GetAllDistribuidoresQuery request, CancellationToken cancellationToken)
        {
            return await _context.Distribuidores.ToListAsync();
        }
    }
} 