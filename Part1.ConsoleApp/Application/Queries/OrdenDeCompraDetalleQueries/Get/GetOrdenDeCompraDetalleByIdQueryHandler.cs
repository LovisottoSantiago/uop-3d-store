using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.OrdenDeCompraDetalleQueries.Get
{
    public class GetOrdenDeCompraDetalleByIdQueryHandler : IRequestHandler<GetOrdenDeCompraDetalleByIdQuery, OrdenDeCompraDetalle>
    {
        private readonly AppDbContext _context;
        public GetOrdenDeCompraDetalleByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompraDetalle> Handle(GetOrdenDeCompraDetalleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.OrdenDeCompraDetalles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
} 