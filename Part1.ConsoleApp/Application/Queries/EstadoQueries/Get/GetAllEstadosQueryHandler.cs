using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.EstadoQueries.Get
{
    public class GetAllEstadosQueryHandler : IRequestHandler<GetAllEstadosQuery, List<Estado>>
    {
        private readonly AppDbContext _context;
        public GetAllEstadosQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Estado>> Handle(GetAllEstadosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Estados.ToListAsync(cancellationToken);
        }
    }
} 