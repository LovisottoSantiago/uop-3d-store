using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class OrdenDeCompraDetalle
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OrdenDeCompra")]
        public int OrdenDeCompraId { get; set; }
        public OrdenDeCompra OrdenDeCompra { get; set; }

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
} 