using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.CobranzaCommands.Create
{
    public class CreateCobranzaCommandHandler : IRequestHandler<CreateCobranzaCommand, Cobranza>
    {
        private readonly AppDbContext _context;
        public CreateCobranzaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cobranza> Handle(CreateCobranzaCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de creación
            return null;
        }
    }
} 