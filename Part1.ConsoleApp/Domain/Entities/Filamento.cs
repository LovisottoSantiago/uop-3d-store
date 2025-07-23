using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class Filamento : Producto
    {
        [ForeignKey("TipoMaterial")]
        public int TipoMaterialId { get; set; }
        public TipoMaterial TipoMaterial { get; set; }

        public required float Peso { get; set; }

    }

}
