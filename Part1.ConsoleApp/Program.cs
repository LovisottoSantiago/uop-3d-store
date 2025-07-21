using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Part1.ConsoleApp.Application.Services;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Part1.ConsoleApp.Repositories;
using Spectre.Console;
using System;

namespace Part1.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = BuildConfiguration();
            var dbContext = CreateDbContext(config);

            if (TestDatabaseConnection(dbContext))
            {
                var productoRepository = new ProductoRepository(dbContext);
                var productoService = new ProductoService(productoRepository);
                
                MostrarMenu(productoService);
            }
        }

        static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        static AppDbContext CreateDbContext(IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new AppDbContext(optionsBuilder.Options);
        }

        static bool TestDatabaseConnection(AppDbContext context)
        {
            try
            {
                context.Database.OpenConnection();
                Console.WriteLine("Conectado a la base de datos exitosamente.");
                context.Database.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        static void MostrarMenu(ProductoService productoService)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOpciones>()
                        .Title("\n[green]Menu de opciones:[/]")
                        .AddChoices(Enum.GetValues<MenuOpciones>())
                );

                switch (opcion)
                {
                    case MenuOpciones.AgregarFilamento:
                        AgregarFilamento(productoService);
                        break;
                    case MenuOpciones.BorrarFilamento:
                        Console.WriteLine("BorrarFilamento");
                        break;
                    case MenuOpciones.ActualizarFilamento:
                        Console.WriteLine("ActualizarFilamento");
                        break;
                    case MenuOpciones.VerFilamento:
                        Console.WriteLine("VerFilamento");
                        break;
                    case MenuOpciones.Salir:
                        Console.WriteLine("Saliendo...");
                        return;
                }
            }
        }
        enum MenuOpciones
        {
            AgregarFilamento,
            BorrarFilamento,
            ActualizarFilamento,
            VerFilamento,
            Salir
        }

        static void AgregarFilamento(ProductoService service)
        {
            Console.Write("Nombre: ");
            var nombre = Console.ReadLine();

            Console.Write("Precio: ");
            var precio = decimal.Parse(Console.ReadLine());

            Console.Write("Peso: ");
            var peso = float.Parse(Console.ReadLine());

            Console.Write("Stock: ");
            var stock = int.Parse(Console.ReadLine());

            Console.Write("Color: ");
            var color = Console.ReadLine();

            Console.Write("ID Tipo Material: ");
            var tipoMaterialId = int.Parse(Console.ReadLine());

            Console.Write("ID Marca: ");
            var marcaId = int.Parse(Console.ReadLine());

            Console.Write("ID Distribuidor: ");
            var distribuidorId = int.Parse(Console.ReadLine());

            //var filamento = "";
           // service.AgregarProductoAsync(filamento);

            Console.WriteLine("Filamento agregado correctamente.");
        }



        static void BorrarFilamento(ProductoService service)
        {

        }

        static void ActualizarFilamento(ProductoService service)
        {

        }

        static void VerFilamentos(ProductoService service)
        {

        }
    }
}
