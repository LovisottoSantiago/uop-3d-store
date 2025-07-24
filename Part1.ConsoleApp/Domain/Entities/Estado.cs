using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Domain.Entities
{
    public class Estado
    {
        [Key]
        public int Id { get; set; }
        public string NombreEstado { get; set; } // Ej: Pendiente, Parcial, Cancelada, Completada
    }
}
