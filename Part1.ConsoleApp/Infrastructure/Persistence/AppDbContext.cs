using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Filamento> Filamentos { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Distribuidor> Distribuidores { get; set; }
        public DbSet<TipoMaterial> TipoMateriales { get; set; }
        public DbSet<DistribuidorMarca> DistribuidorMarcas { get; set; }
        public DbSet<Cobranza> Cobranzas {  get; set; }
        public DbSet<OrdenDeCompra> OrdenDeCompras { get; set; }
        public DbSet<OrdenDeCompraDetalle> OrdenDeCompraDetalles { get; set; }
        public DbSet<Estado> Estados { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cobranza>()
                .Property(c => c.MontoPagado)
                .HasPrecision(18, 2); 

            modelBuilder.Entity<OrdenDeCompraDetalle>()
                .Property(o => o.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .HasDiscriminator<string>("TipoProducto")
                .HasValue<Filamento>("Filamento")
                .HasValue<Insumo>("Insumo");

            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Distribuidor>().ToTable("Distribuidor");
            modelBuilder.Entity<TipoMaterial>().ToTable("TipoMaterial");
            modelBuilder.Entity<DistribuidorMarca>().ToTable("DistribuidorMarca");
            modelBuilder.Entity<Cobranza>().ToTable("Cobranza");
            modelBuilder.Entity<OrdenDeCompra>().ToTable("OrdenDeCompra");
            modelBuilder.Entity<OrdenDeCompraDetalle>().ToTable("OrdenDeCompraDetalle");
            modelBuilder.Entity<Estado>().ToTable("Estado");

            modelBuilder.Entity<DistribuidorMarca>()
                  .HasOne(dm => dm.Marca)
                  .WithMany(m => m.DistribuidorMarcas)
                  .HasForeignKey(dm => dm.MarcaId);

            modelBuilder.Entity<DistribuidorMarca>()
                .HasOne(dm => dm.Distribuidor)
                .WithMany(d => d.DistribuidorMarcas)
                .HasForeignKey(dm => dm.DistribuidorId);

            modelBuilder.Entity<Filamento>()
                .HasOne(f => f.TipoMaterial)
                .WithMany()
                .HasForeignKey(f => f.TipoMaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cobranza>()
                .HasOne(c => c.Estado)
                .WithMany()
                .HasForeignKey(c => c.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
