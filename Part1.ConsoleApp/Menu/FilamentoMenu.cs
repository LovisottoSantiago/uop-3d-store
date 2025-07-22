using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class FilamentoMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext dbContext)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<FilamentoOpciones>()
                        .Title("\n[blue]Filamentos:[/]")
                        .AddChoices(Enum.GetValues<FilamentoOpciones>())
                );

                switch (opcion)
                {
                    case FilamentoOpciones.Agregar:
                        await AgregarFilamento(mediator, dbContext);
                        break;
                    case FilamentoOpciones.Borrar:
                        Console.WriteLine("BorrarFilamento");
                        break;
                    case FilamentoOpciones.Actualizar:
                        Console.WriteLine("ActualizarFilamento");
                        break;
                    case FilamentoOpciones.Ver:
                        Console.WriteLine("VerFilamento");
                        break;
                    case FilamentoOpciones.Volver:
                        return;
                }
            }
        }

        enum FilamentoOpciones
        {
            Agregar,
            Borrar,
            Actualizar,
            Ver,
            Volver
        }

        private static async Task AgregarFilamento(IMediator mediator, AppDbContext dbContext)
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

            var command = new Application.Commands.FilamentoCommands.Create.CreateFilamentoCommand
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
    }
} 