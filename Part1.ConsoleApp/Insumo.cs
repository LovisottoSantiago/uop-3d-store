using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp
{
    internal class Insumo
    {
        [ForeignKey("Distribuidor")]
        public int DistribuidorId { get; set; }
        public required Distribuidor Distribuidor { get; set; }
    }
}
