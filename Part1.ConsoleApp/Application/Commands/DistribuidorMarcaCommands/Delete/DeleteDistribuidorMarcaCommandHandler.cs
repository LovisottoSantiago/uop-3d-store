using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Delete
{
    public class DeleteDistribuidorMarcaCommandHandler : IRequestHandler<DeleteDistribuidorMarcaCommand, DistribuidorMarca>
    {
        private readonly AppDbContext _context;
        public DeleteDistribuidorMarcaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DistribuidorMarca> Handle(DeleteDistribuidorMarcaCommand request, CancellationToken cancellationToken)
        {
            var distribuidorMarca = _context.DistribuidorMarcas.FirstOrDefault(d => d.Id ==  request.Id);

            if (distribuidorMarca == null)
            {
                return default;
            }

            _context.Remove(distribuidorMarca);
            await _context.SaveChangesAsync();
            return distribuidorMarca;
        }
    }
} 