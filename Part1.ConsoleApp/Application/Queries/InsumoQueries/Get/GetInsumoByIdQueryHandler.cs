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
    public class GetFilamentoByIdQueryHandler : IRequestHandler<GetInsumoByIdQuery, Insumo>
    {
        private readonly AppDbContext _context;

        public GetFilamentoByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Insumo> Handle(GetInsumoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Insumos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    
    }
}
