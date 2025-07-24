using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.OrdenDeCompraDetalleQueries.Get
{
    public class GetAllOrdenesDeCompraDetalleQueryHandler : IRequestHandler<GetAllOrdenesDeCompraDetalleQuery, List<OrdenDeCompraDetalle>>
    {
        private readonly AppDbContext _context;
        public GetAllOrdenesDeCompraDetalleQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrdenDeCompraDetalle>> Handle(GetAllOrdenesDeCompraDetalleQuery request, CancellationToken cancellationToken)
        {
            return await _context.OrdenDeCompraDetalles.ToListAsync(cancellationToken);
        }
    }
} 