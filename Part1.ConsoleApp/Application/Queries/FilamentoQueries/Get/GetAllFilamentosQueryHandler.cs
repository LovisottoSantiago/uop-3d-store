using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Queries.FilamentoQueries.Get
{
    public class GetAllFilamentosQueryHandler : IRequestHandler<GetAllFilamentosQuery, IEnumerable<Filamento>>
    {
        private readonly AppDbContext _context;

        public GetAllFilamentosQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Filamento>> Handle(GetAllFilamentosQuery request, CancellationToken cancellationToken)
        {
            return await _context.Filamentos.ToListAsync();
        }

    }
}
