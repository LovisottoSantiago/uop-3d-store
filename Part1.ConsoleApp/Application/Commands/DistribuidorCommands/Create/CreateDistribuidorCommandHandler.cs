using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.DistribuidorCommands.Create
{
    public class CreateDistribuidorCommandHandler : IRequestHandler<CreateDistribuidorCommand, Distribuidor>
    {
        private readonly AppDbContext _context;
        public CreateDistribuidorCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Distribuidor> Handle(CreateDistribuidorCommand request, CancellationToken cancellationToken)
        {
            var distribuidor = new Distribuidor
            {
                Nombre = request.Nombre,
                Direccion = request.Direccion,
                Telefono = request.Telefono
            };
            _context.Distribuidores.Add(distribuidor);
            await _context.SaveChangesAsync();
            return distribuidor;
        }
    }
} 