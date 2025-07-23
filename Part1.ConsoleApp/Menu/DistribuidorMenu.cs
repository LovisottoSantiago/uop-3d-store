using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class DistribuidorMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext dbContext)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<DistribuidorOpciones>()
                        .Title("\n[blue]Distribuidores:[/]")
                        .AddChoices(Enum.GetValues<DistribuidorOpciones>())
                );

                switch (opcion)
                {
                    case DistribuidorOpciones.Agregar:
                        await AgregarDistribuidor(mediator);
                        break;
                    case DistribuidorOpciones.Listar:
                        await ListarDistribuidores(mediator);
                        break;
                    case DistribuidorOpciones.Volver:
                        return;
                }
            }
        }

        enum DistribuidorOpciones
        {
            Agregar,
            Listar,
            Volver
        }

        private static async Task AgregarDistribuidor(IMediator mediator)
        {
            var nombre = AnsiConsole.Ask<string>("Nombre del distribuidor:");
            var direccion = AnsiConsole.Ask<string>("Dirección del distribuidor:");
            var telefono = AnsiConsole.Ask<long>("Teléfono del distribuidor:");
            var command = new Application.Commands.DistribuidorCommands.Create.CreateDistribuidorCommand
            {
                Nombre = nombre,
                Direccion = direccion,
                Telefono = telefono
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
                AnsiConsole.MarkupLine($"[green]Distribuidor creado con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al crear el distribuidor.[/]");
        }

        private static async Task ListarDistribuidores(IMediator mediator)
        {
            var distribuidores = await mediator.Send(new Application.Queries.DistribuidorQueries.Get.GetAllDistribuidoresQuery());
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var distribuidor in distribuidores)
            {
                table.AddRow(distribuidor.Id.ToString(), distribuidor.Nombre);
            }
            AnsiConsole.Write(table);
        }
    }
} 