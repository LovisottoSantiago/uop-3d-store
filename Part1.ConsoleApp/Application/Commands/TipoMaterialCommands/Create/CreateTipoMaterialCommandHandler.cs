using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Create
{
    public class CreateTipoMaterialCommandHandler : IRequestHandler<CreateTipoMaterialCommand, TipoMaterial>
    {
        private readonly AppDbContext _context;
        public CreateTipoMaterialCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TipoMaterial> Handle(CreateTipoMaterialCommand request, CancellationToken cancellationToken)
        {
            var tipo = new TipoMaterial
            {
                Nombre = request.Nombre
            };

            _context.TipoMateriales.Add(tipo);
            await _context.SaveChangesAsync();
            return tipo;
        }
    }
} 