using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Part1.ConsoleApp.Application.Commands.InsumoCommands.Create
{
    public class CreateInsumoCommandHandler : IRequestHandler<CreateInsumoCommand, Insumo>
    {
        private readonly AppDbContext _context;
        public CreateInsumoCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Insumo> Handle(CreateInsumoCommand request, CancellationToken cancellationToken)
        {
            var insumo = new Insumo
            {
                Nombre = request.Nombre,
                Precio = (decimal)request.Precio,
                Stock = (int)request.Stock,
                Estado = request.Estado,
                Color = request.Color,
                MarcaId = request.MarcaId,
                DistribuidorId = request.DistribuidorId
            };

            _context.Insumos.Add(insumo);
            await _context.SaveChangesAsync();
            return insumo;
        }

    }
}
