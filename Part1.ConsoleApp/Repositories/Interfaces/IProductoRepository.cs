using Part1.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Repositories.Interfaces
{
    internal interface IProductoRepository
    {
        Task<List<Producto>> GetAll();
        Task<Producto?> GetByIdAsync(int id);
        Task<int> CreateAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsExistByIdAsync(int id);

    }
}
