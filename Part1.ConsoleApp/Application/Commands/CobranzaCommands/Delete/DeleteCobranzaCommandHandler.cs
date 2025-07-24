using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Delete
{
    public class DeleteCobranzaCommandHandler : IRequestHandler<DeleteCobranzaCommand, Cobranza>
    {
        private readonly AppDbContext _context;
        public DeleteCobranzaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cobranza> Handle(DeleteCobranzaCommand request, CancellationToken cancellationToken)
        {
            var cobranza = _context.Cobranzas.FirstOrDefault(c => c.Id == request.Id);

            if (cobranza == null)
            {
                return default;
            }

            _context.Remove(cobranza);
            await _context.SaveChangesAsync();
            return cobranza;
        }
    }
} 