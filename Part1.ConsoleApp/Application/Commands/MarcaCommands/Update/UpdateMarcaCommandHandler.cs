using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.MarcaCommands.Update
{
    public class UpdateMarcaCommandHandler : IRequestHandler<UpdateMarcaCommand, Marca>
    {
        private readonly AppDbContext _context;
        public UpdateMarcaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Marca> Handle(UpdateMarcaCommand request, CancellationToken cancellationToken)
        {
            var marca = _context.Marcas.FirstOrDefault(m => m.Id == request.Id);

            if (marca == null)
            {
                return default;
            }

            marca.Nombre = request.Nombre;
            marca.DistribuidorId = request.DistribuidorId;

            await _context.SaveChangesAsync();
            return marca;
        }
    }
} 