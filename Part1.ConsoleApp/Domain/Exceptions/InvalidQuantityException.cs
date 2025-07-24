using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Domain.Exceptions
{
    public class InvalidQuantityException : BusinessException
    {
        public InvalidQuantityException() : base("La cantidad no puede ser menor a 0.") { }
    }
}
