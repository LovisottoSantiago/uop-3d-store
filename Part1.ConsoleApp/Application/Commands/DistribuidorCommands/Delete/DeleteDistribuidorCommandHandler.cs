using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Delete
{
    public class DeleteDistribuidorCommandHandler : IRequestHandler<DeleteDistribuidorCommand, Distribuidor>
    {
        private readonly AppDbContext _context;
        public DeleteDistribuidorCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Distribuidor> Handle(DeleteDistribuidorCommand request, CancellationToken cancellationToken)
        {
            var distribuidor = _context.Distribuidores.FirstOrDefault(d => d.Id == request.Id);

            if (distribuidor == null)
            {
                return default;
            }

            _context.Remove(distribuidor);
            await _context.SaveChangesAsync();
            return distribuidor;
        }
    }
} 