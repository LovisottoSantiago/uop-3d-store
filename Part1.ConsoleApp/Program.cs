using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Part1.ConsoleApp.Infrastructure.Persistence;
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
                MostrarMenu();
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

        static void MostrarMenu()
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
                        Console.WriteLine("Agregar filamento.");
                        break;
                    case MenuOpciones.BorrarFilamento:
                        Console.WriteLine("Borrar filamento.");
                        break;
                    case MenuOpciones.ActualizarFilamento:
                        Console.WriteLine("Actualizar filamento.");
                        break;
                    case MenuOpciones.VerFilamento:
                        Console.WriteLine("Ver filamento.");
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
    }
}
