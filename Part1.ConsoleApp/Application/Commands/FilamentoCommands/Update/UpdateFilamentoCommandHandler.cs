using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Part1.ConsoleApp.Application.Commands.FilamentoCommands.Update
{
    public class UpdateFilamentoCommandHandler : IRequestHandler<UpdateFilamentoCommand, Filamento>
    {
        private readonly AppDbContext _context;

        public UpdateFilamentoCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Filamento> Handle(UpdateFilamentoCommand request, CancellationToken cancellationToken)
        {
            var filamento = _context.Filamentos.FirstOrDefault(f => f.Id == request.Id);

            if (filamento == null)
            {
                return default;
            }

            filamento.Nombre = request.Nombre;
            filamento.Precio = (decimal)request.Precio;
            filamento.Stock = (int)request.Stock;
            filamento.Estado = request.Estado;
            filamento.Color = request.Color;
            filamento.MarcaId = request.MarcaId;
            filamento.TipoMaterialId = request.TipoMaterialId;
            filamento.Peso = (float)request.Peso;
            filamento.ImagenUrl = request.ImagenUrl;

            await _context.SaveChangesAsync();
            return filamento;
        }

    }
}
