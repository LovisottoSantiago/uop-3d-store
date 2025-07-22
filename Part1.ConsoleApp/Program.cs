using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Part1.ConsoleApp.Application.Commands.FilamentoCommands.Create;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Part1.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            // Configurar DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Registrar MediatR 
            services.AddMediatR(typeof(CreateFilamentoCommandHandler).Assembly);

            var provider = services.BuildServiceProvider();
            var dbContext = provider.GetRequiredService<AppDbContext>();
            var mediator = provider.GetRequiredService<IMediator>();

            if (TestDatabaseConnection(dbContext))
            {
                await MostrarMenu(mediator, dbContext);
            }
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

        static async Task MostrarMenu(IMediator mediator, AppDbContext dbContext)
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
                        await AgregarFilamento(mediator, dbContext);
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

        static async Task AgregarFilamento(IMediator mediator, AppDbContext dbContext)
        {
            Console.WriteLine("\n[green]Agregar Filamento:");
            var nombre = AnsiConsole.Ask<string>("Nombre:");
            var precio = AnsiConsole.Ask<decimal>("Precio:");
            var peso = AnsiConsole.Ask<float>("Peso:");
            var stock = AnsiConsole.Ask<int>("Stock:");
            var estado = AnsiConsole.Confirm("¿Está activo?", true);
            var color = AnsiConsole.Ask<string>("Color:");

            var tipos = dbContext.TipoMateriales.ToList();
            var tipoMaterial = AnsiConsole.Prompt(
                new SelectionPrompt<TipoMaterial>()
                    .Title("Seleccione el tipo de material:")
                    .AddChoices(tipos)
                    .UseConverter(t => $"{t.Id} - {t.Nombre}")
            );

            var marcas = dbContext.Marcas.ToList();
            var marca = AnsiConsole.Prompt(
                new SelectionPrompt<Marca>()
                    .Title("Seleccione la marca:")
                    .AddChoices(marcas)
                    .UseConverter(m => $"{m.Id} - {m.Nombre}")
            );

            var distribuidores = dbContext.Distribuidores.ToList();
            var distribuidor = AnsiConsole.Prompt(
                new SelectionPrompt<Distribuidor>()
                    .Title("Seleccione el distribuidor:")
                    .AddChoices(distribuidores)
                    .UseConverter(d => $"{d.Id} - {d.Nombre}")
            );

            var command = new CreateFilamentoCommand
            {
                Nombre = nombre,
                Precio = precio,
                Peso = peso,
                Stock = stock,
                Estado = estado,
                Color = color,
                TipoMaterialId = tipoMaterial.Id,
                MarcaId = marca.Id,
                DistribuidorId = distribuidor.Id
            };

            var resultado = await mediator.Send(command);

            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Filamento creado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al crear el filamento.[/]");
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
