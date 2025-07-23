using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.InsumoCommands.Update
{
    public class UpdateInsumoCommandHandler : IRequestHandler<UpdateInsumoCommand, Insumo>
    {
        private readonly AppDbContext _context;

        public UpdateInsumoCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Insumo> Handle(UpdateInsumoCommand request, CancellationToken cancellationToken)
        {
            var insumo = _context.Insumos.FirstOrDefault(ins => ins.Id  == request.Id);

            if (insumo == null)
            {
                return default;
            }

            insumo.Nombre = request.Nombre;
            insumo.Precio = (decimal)request.Precio;
            insumo.Stock = (int)request.Stock;
            insumo.Estado = request.Estado;
            insumo.Color = request.Color;
            insumo.MarcaId = request.MarcaId;
            insumo.DistribuidorId = request.DistribuidorId;
            
            await _context.SaveChangesAsync();
            return insumo;
        }

    }
}
