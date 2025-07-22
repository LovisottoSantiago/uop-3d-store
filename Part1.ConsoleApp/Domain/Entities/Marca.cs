using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public required string Nombre { get; set; }
        
        [ForeignKey("Distribuidor")]        
        public int DistribuidorId { get; set; }
        public required Distribuidor Distribuidor { get; set; }

        public List<Filamento> Filamentos { get; set; } = new();
    }
}
