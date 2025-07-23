using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.MarcaQueries.Get
{
    public class GetMarcaByIdQueryHandler : IRequestHandler<GetMarcaByIdQuery, Marca>
    {
        private readonly AppDbContext _context;
        public GetMarcaByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Marca> Handle(GetMarcaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Marcas.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
} 