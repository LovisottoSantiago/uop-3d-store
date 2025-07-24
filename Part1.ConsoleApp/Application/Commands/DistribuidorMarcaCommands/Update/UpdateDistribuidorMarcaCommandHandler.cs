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
            var distribuidorMarca = _context.DistribuidorMarcas.FirstOrDefault(d => d.Id == request.Id);

            if (distribuidorMarca == null)
            {
                return default;
            }

            distribuidorMarca.DistribuidorId = request.DistribuidorId;
            distribuidorMarca.MarcaId = request.MarcaId;

            await _context.SaveChangesAsync();
            return distribuidorMarca;
        }
    }
} 