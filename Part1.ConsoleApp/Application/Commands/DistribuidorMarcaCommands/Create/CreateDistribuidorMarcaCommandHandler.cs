using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorMarcaCommands.Create
{
    public class CreateDistribuidorMarcaCommandHandler : IRequestHandler<CreateDistribuidorMarcaCommand, DistribuidorMarca>
    {
        private readonly AppDbContext _context;
        public CreateDistribuidorMarcaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DistribuidorMarca> Handle(CreateDistribuidorMarcaCommand request, CancellationToken cancellationToken)
        {
            var distribuidorMarca = new DistribuidorMarca
            {
                DistribuidorId = request.DistribuidorId,
                MarcaId = request.MarcaId
            };

            _context.Add(distribuidorMarca);
            await _context.SaveChangesAsync();
            return distribuidorMarca;
        }
    }
} 