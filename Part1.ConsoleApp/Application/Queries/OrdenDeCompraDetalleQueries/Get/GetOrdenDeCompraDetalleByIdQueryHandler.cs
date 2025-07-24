using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

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
            // TODO: Implementar lógica de consulta por Id
            return null;
        }
    }
} 