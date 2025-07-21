using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Services
{
    internal class ProductoService
    {
        private readonly IProductoRepository _producto;

        public ProductoService(IProductoRepository producto)
        {
            _producto = producto;
        }

        public Task<int> AgregarProductoAsync(Producto producto)
        {
            if (producto.Stock < 0)
                throw new ArgumentException("El stock no puede ser negativo.");

            return _producto.CreateAsync(producto);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _producto.DeleteAsync(id);
        }

        public Task<List<Producto>> GetAll()
        {
            return _producto.GetAll();
        }

        public Task<Producto?> GetByIdAsync(int id)
        {
            return _producto.GetByIdAsync(id);
        }

        public Task<bool> IsExistByIdAsync(int id)
        {
            return _producto.IsExistByIdAsync(id);
        }

        public Task<bool> UpdateAsync(Producto producto)
        {
            return _producto.UpdateAsync(producto);
        }
    }
}
