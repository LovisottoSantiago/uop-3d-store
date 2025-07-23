using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.FilamentoCommands.Create
{
    internal class CreateFilamentoCommand : IRequest<Filamento>
    {
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }
        public int? Stock { get; set; }
        public bool Estado { get; set; } = true;
        public string? Color { get; set; }
        public int MarcaId { get; set; }
        public int DistribuidorId { get; set; }
        public int TipoMaterialId { get; set; }
        public float? Peso { get; set; }
    }

}
