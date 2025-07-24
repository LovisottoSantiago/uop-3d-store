using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class OrdenDeCompra
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } // Ej: Pendiente, Pagada, Cancelada
        public List<OrdenDeCompraDetalle> Detalles { get; set; } = new();
    }
} 