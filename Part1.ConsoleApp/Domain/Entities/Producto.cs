using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Domain.Entities
{
    public abstract class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required decimal Precio { get; set; }

        [Required]
        public required int Stock { get; set; }
        [Required]
        public required bool Estado { get; set; }
        
        [Required]
        public required string Color { get; set; }
        [Required]
        public required string ImagenUrl { get; set; }

        [ForeignKey("Marca")]
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

        [ForeignKey("Distribuidor")]
        public int DistribuidorId { get; set; }
        public Distribuidor Distribuidor { get; set; }
    }

}
