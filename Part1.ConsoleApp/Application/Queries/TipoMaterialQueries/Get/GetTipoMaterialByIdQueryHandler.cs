using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Data.Entity;

namespace Part1.ConsoleApp.Application.Queries.TipoMaterialQueries.Get
{
    public class GetTipoMaterialByIdQueryHandler : IRequestHandler<GetTipoMaterialByIdQuery, TipoMaterial>
    {
        private readonly AppDbContext _context;
        public GetTipoMaterialByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TipoMaterial> Handle(GetTipoMaterialByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.TipoMateriales.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
} 