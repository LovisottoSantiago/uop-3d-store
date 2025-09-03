using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Part1.ConsoleApp.Application.Queries.FilamentoQueries.Get
{
    public class GetFilamentoByIdQueryHandler : IRequestHandler<GetFilamentoByIdQuery, Filamento>
    {
        private readonly AppDbContext _context;

        public GetFilamentoByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Filamento> Handle(GetFilamentoByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Filamentos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    
    }
}
