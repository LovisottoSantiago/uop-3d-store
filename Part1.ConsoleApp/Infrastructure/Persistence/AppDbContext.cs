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

            modelBuilder.Entity<Producto>()
                .HasDiscriminator<string>("TipoProducto")
                .HasValue<Filamento>("Filamento")
                .HasValue<Insumo>("Insumo");

            modelBuilder.Entity<Producto>().ToTable("Producto");
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Distribuidor>().ToTable("Distribuidor");
            modelBuilder.Entity<TipoMaterial>().ToTable("TipoMaterial");

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Distribuidor)
                .WithMany(d => d.Productos)
                .HasForeignKey(p => p.DistribuidorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Marca)
                .WithMany(m => m.Productos) 
                .HasForeignKey(p => p.MarcaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Filamento>()
                .HasOne(f => f.TipoMaterial)
                .WithMany()
                .HasForeignKey(f => f.TipoMaterialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
