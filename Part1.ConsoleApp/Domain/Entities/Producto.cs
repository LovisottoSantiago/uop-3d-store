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
        public required float Peso { get; set; }

        [Required]
        public required int Stock { get; set; }
    }

}
