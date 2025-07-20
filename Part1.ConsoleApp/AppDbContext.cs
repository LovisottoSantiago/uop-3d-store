using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Filamento> Filamentos { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Distribuidor> Distribuidores { get; set; }
        public DbSet<TipoMaterial> TiposMaterial { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Producto es una clase abstracta
            modelBuilder.Entity<Producto>()
            .HasDiscriminator<string>("TipoProducto")
            .HasValue<Filamento>("Filamento")
            .HasValue<Insumo>("Insumo");

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
