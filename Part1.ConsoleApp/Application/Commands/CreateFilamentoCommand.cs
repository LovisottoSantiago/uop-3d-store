using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.UseCases
{
    internal class CreateFilamentoCommand
    {
        private readonly AppDbContext _context;

        public CreateFilamentoCommand(AppDbContext context)
        {
            _context = context;
        }

        public void Execute(string nombre, decimal precio, float peso, int stock, string color, int tipoMaterialId, int marcaId, int distribuidorId)
        {
            var filamento = new Filamento
            {
                Nombre = nombre,
                Precio = precio,
                Peso = peso,
                Stock = stock,
                Color = color,
                TipoMaterialId = tipoMaterialId,
                MarcaId = marcaId,
                DistribuidorId = distribuidorId
            };

            _context.Filamentos.Add(filamento);
            _context.SaveChanges();
        }




    }
}
