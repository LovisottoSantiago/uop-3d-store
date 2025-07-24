using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Update
{
    public class UpdateDistribuidorMarcaCommandHandler : IRequestHandler<UpdateDistribuidorMarcaCommand, DistribuidorMarca>
    {
        private readonly AppDbContext _context;
        public UpdateDistribuidorMarcaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DistribuidorMarca> Handle(UpdateDistribuidorMarcaCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de actualización
            return null;
        }
    }
} 