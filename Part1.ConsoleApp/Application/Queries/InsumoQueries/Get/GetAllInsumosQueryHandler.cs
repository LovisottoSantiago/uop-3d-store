using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Queries.InsumoQueries.Get
{
    public class GetAllInsumosQueryHandler : IRequestHandler<GetAllInsumosQuery, IEnumerable<Insumo>>
    {
        private readonly AppDbContext _context;

        public GetAllInsumosQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Insumo>> Handle(GetAllInsumosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Insumos.ToListAsync();
        }

    }
}
