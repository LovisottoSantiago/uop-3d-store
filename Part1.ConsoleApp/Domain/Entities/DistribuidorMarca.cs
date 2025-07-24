using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class DistribuidorMarca
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Distribuidor")]
        public int DistribuidorId { get; set; }
        public Distribuidor Distribuidor { get; set; }

        [ForeignKey("Marca")]
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }
    }
} 