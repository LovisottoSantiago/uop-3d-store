using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Domain.Entities
{
    internal class Filamento : Producto
    {
        public required string Color { get; set; }

        [ForeignKey("TipoMaterial")]
        public int TipoMaterialId { get; set; }
        public TipoMaterial TipoMaterial { get; set; }

        [ForeignKey("Marca")]
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        [ForeignKey("Distribuidor")]
        public int DistribuidorId { get; set; }
        public Distribuidor Distribuidor { get; set; }

        
    }

}
