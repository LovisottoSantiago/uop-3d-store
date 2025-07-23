using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Data.Entity;

namespace Part1.ConsoleApp.Application.Queries.MarcaQueries.Get
{
    public class GetAllMarcasQueryHandler : IRequestHandler<GetAllMarcasQuery, IEnumerable<Marca>>
    {
        private readonly AppDbContext _context;
        public GetAllMarcasQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Marca>> Handle(GetAllMarcasQuery request, CancellationToken cancellationToken)
        {
            return await _context.Marcas.ToListAsync();
        }
    }
} 