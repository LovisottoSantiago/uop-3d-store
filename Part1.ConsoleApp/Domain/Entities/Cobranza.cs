using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class Cobranza
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OrdenDeCompra")]
        public int OrdenDeCompraId { get; set; }
        public OrdenDeCompra OrdenDeCompra { get; set; }

        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string Estado { get; set; } // Ej: Pagada, Parcial, Pendiente
    }
} 