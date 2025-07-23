using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.InsumoCommands.Update
{
    public class UpdateInsumoCommand : IRequest<Insumo>
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public bool Estado { get; set; } = true;
        public string? Color { get; set; }
        public int MarcaId { get; set; }
        public int DistribuidorId { get; set; }
    }
}
