using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Queries.OrdenDeCompraQueries.Get
{
    public class GetOrdenDeCompraByIdQueryHandler : IRequestHandler<GetOrdenDeCompraByIdQuery, OrdenDeCompra>
    {
        private readonly AppDbContext _context;
        public GetOrdenDeCompraByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompra> Handle(GetOrdenDeCompraByIdQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de consulta por Id
            return null;
        }
    }
} 