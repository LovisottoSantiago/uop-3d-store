using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.InsumoCommands.Create
{
    public class CreateInsumoCommand : IRequest<Insumo>
    {
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public bool Estado { get; set; } = true;
        public string? Color { get; set; }
        public int MarcaId { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
