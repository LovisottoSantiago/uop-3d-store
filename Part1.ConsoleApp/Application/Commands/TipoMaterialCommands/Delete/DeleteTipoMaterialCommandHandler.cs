using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Delete
{
    public class DeleteTipoMaterialCommandHandler : IRequestHandler<DeleteTipoMaterialCommand, TipoMaterial>
    {
        private readonly AppDbContext _context;
        public DeleteTipoMaterialCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TipoMaterial> Handle(DeleteTipoMaterialCommand request, CancellationToken cancellationToken)
        {
            var tipoMaterial = _context.TipoMateriales.FirstOrDefault(t => t.Id == request.Id);

            if (tipoMaterial == null)
            {
                return default;
            }

            _context.Remove(tipoMaterial);
            await _context.SaveChangesAsync();
            return tipoMaterial;
        }
    }
} 