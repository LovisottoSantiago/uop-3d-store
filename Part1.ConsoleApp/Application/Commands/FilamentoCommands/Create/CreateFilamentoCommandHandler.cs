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

        public async Task<Filamento> Handle(CreateFilamentoCommand command, CancellationToken cancellationToken)
        {
            var filamento = new Filamento
            {
                Nombre = command.Nombre,
                Precio = (decimal)command.Precio,
                Peso = (float)command.Peso,
                Stock = (int)command.Stock,
                Estado = command.Estado,
                Color = command.Color,
                TipoMaterialId = command.TipoMaterialId,
                MarcaId = command.MarcaId,
                DistribuidorId = command.DistribuidorId
            };

            _context.Filamentos.Add(filamento);
            await _context.SaveChangesAsync();
            return filamento;
        }

    }
}
