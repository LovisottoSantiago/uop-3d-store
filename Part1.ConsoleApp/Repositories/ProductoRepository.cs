using Microsoft.EntityFrameworkCore;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Part1.ConsoleApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Repositories
{
    internal class ProductoRepository : IProductoRepository
    {
        #region Constructor
        private readonly AppDbContext _context;
        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        public async Task<int> CreateAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
            return producto.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Productos.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Producto>> GetAll()
        {
            var list = await _context.Productos.ToListAsync();
            return list;
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            var product = await _context.Productos.Where(q => q.Id == id).FirstOrDefaultAsync();
            return product;
        }

        public async Task<bool> IsExistByIdAsync(int id)
        {
            var product = await GetByIdAsync(id);
            return product != null;
        }

        public async Task<bool> UpdateAsync(Producto producto)
        {
            _context.Entry(producto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
