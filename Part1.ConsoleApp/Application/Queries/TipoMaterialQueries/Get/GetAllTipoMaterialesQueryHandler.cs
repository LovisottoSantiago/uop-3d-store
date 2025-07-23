using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Data.Entity;

namespace Part1.ConsoleApp.Application.Queries.TipoMaterialQueries.Get
{
    public class GetAllTipoMaterialesQueryHandler : IRequestHandler<GetAllTipoMaterialesQuery, IEnumerable<TipoMaterial>>
    {
        private readonly AppDbContext _context;
        public GetAllTipoMaterialesQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TipoMaterial>> Handle(GetAllTipoMaterialesQuery request, CancellationToken cancellationToken)
        {
            return await _context.TipoMateriales.ToListAsync();
        }
    }
} 