using MediatR;
using Part1.ConsoleApp.Application.Commands.MarcaCommands.Update;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.TipoMaterialCommands.Update
{
    public class UpdateTipoMaterialCommandhandler : IRequestHandler<UpdateTipoMaterialCommand, TipoMaterial>
    {
        private readonly AppDbContext _context;
        public UpdateTipoMaterialCommandhandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TipoMaterial> Handle(UpdateTipoMaterialCommand request, CancellationToken cancellationToken)
        {
            var tipoMaterial = _context.TipoMateriales.FirstOrDefault(t => t.Id == request.Id);

            if (tipoMaterial == null)
            {
                return default;
            }

            tipoMaterial.Nombre = request.Nombre;
            await _context.SaveChangesAsync();
            return tipoMaterial;
        }
    }
}
