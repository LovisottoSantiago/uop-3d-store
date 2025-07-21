using Microsoft.EntityFrameworkCore;
using Part1.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Infrastructure.Persistence
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Filamento> Filamentos { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Distribuidor> Distribuidores { get; set; }
        public DbSet<TipoMaterial> TipoMateriales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Producto es una clase abstracta.
            modelBuilder.Entity<Producto>()
                .HasDiscriminator<string>("TipoProducto")
                .HasValue<Filamento>("Filamento")
                .HasValue<Insumo>("Insumo");
           

            // Nomenclatura de las entidades, quitar el plural para respetar nombres.
            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Distribuidor>().ToTable("Distribuidor");
            modelBuilder.Entity<TipoMaterial>().ToTable("TipoMaterial");

            // Relaciones conflictivas: Distribuidor en ambas subclases
            modelBuilder.Entity<Filamento>()
                .HasOne(f => f.Distribuidor)
                .WithMany()
                .HasForeignKey(f => f.DistribuidorId)
                .OnDelete(DeleteBehavior.Restrict); // desactiva cascade

            modelBuilder.Entity<Insumo>()
                .HasOne(i => i.Distribuidor)
                .WithMany()
                .HasForeignKey(i => i.DistribuidorId)
                .OnDelete(DeleteBehavior.Restrict); // desactiva cascade

        }

    }
}
