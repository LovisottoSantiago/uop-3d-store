using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class Distribuidor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public required string Nombre { get; set; }
        public long Telefono { get; set; }
        public required string Direccion {  get; set; }
        public List<DistribuidorMarca> DistribuidorMarcas { get; set; } = new();

    }
}
