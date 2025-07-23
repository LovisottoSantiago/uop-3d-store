using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Update
{
    public class UpdateDistribuidorCommandHandler : IRequestHandler<UpdateDistribuidorCommand, Distribuidor>
    {
        private readonly AppDbContext _context;
        public UpdateDistribuidorCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Distribuidor> Handle(UpdateDistribuidorCommand request, CancellationToken cancellationToken)
        {
            var distribuidor = _context.Distribuidores.FirstOrDefault(d => d.Id == request.Id);

            if (distribuidor == null)
            {
                return default;
            }

            distribuidor.Nombre = request.Nombre;
            distribuidor.Telefono = request.Telefono;
            distribuidor.Direccion = request.Direccion;

            await _context.SaveChangesAsync();
            return distribuidor;
        }
    }
} 