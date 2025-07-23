using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.FilamentoCommands.Create
{
    internal class CreateFilamentoCommandHandler : IRequestHandler<CreateFilamentoCommand, Filamento>
    {
        private readonly AppDbContext _context;

        public CreateFilamentoCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Filamento> Handle(CreateFilamentoCommand request, CancellationToken cancellationToken)
        {
            var filamento = new Filamento
            {
                Nombre = request.Nombre,
                Precio = (decimal)request.Precio,
                Stock = (int)request.Stock,
                Estado = request.Estado,
                Color = request.Color,
                MarcaId = request.MarcaId,
                DistribuidorId = request.DistribuidorId,
                TipoMaterialId = request.TipoMaterialId,
                Peso = (float)request.Peso
            };

            _context.Filamentos.Add(filamento);
            await _context.SaveChangesAsync();
            return filamento;
        }

    }
}
