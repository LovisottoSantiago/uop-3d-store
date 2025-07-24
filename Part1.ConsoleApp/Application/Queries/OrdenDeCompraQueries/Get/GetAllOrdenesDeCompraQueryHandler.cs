using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Part1.ConsoleApp.Application.Queries.OrdenDeCompraQueries.Get
{
    public class GetAllOrdenesDeCompraQueryHandler : IRequestHandler<GetAllOrdenesDeCompraQuery, List<OrdenDeCompra>>
    {
        private readonly AppDbContext _context;
        public GetAllOrdenesDeCompraQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrdenDeCompra>> Handle(GetAllOrdenesDeCompraQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de consulta
            return null;
        }
    }
} 