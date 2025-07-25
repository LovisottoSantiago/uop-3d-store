using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Part1.ConsoleApp.Menu
{
    public static class DistribuidorMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
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
                    case DistribuidorOpciones.Editar:
                        await EditarDistribuidor(mediator);
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
            Editar,
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
            AnsiConsole.MarkupLine($"[yellow]Cantidad de distribuidores encontrados: {distribuidores?.Count() ?? 0}[/]");
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var distribuidor in distribuidores)
            {
                table.AddRow(distribuidor.Id.ToString(), distribuidor.Nombre);
            }
            AnsiConsole.Write(table);
        }

        private static async Task EditarDistribuidor(IMediator mediator)
        {
            var distribuidores = await mediator.Send(new Application.Queries.DistribuidorQueries.Get.GetAllDistribuidoresQuery());
            if (distribuidores == null || !distribuidores.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay distribuidores para editar.[/]");
                return;
            }
            var distribuidor = AnsiConsole.Prompt(
                new SelectionPrompt<Distribuidor>()
                    .Title("Seleccione el distribuidor a editar:")
                    .AddChoices(distribuidores)
                    .UseConverter(d => $"{d.Id} - {d.Nombre}")
            );
            var nuevoNombre = AnsiConsole.Ask<string>("Nuevo nombre del distribuidor:", distribuidor.Nombre);
            var nuevaDireccion = AnsiConsole.Ask<string>("Nueva dirección del distribuidor:", distribuidor.Direccion);
            var nuevoTelefono = AnsiConsole.Ask<long>("Nuevo teléfono del distribuidor:", distribuidor.Telefono);
            var command = new Application.Commands.DistribuidorCommands.Update.UpdateDistribuidorCommand
            {
                Id = distribuidor.Id,
                Nombre = nuevoNombre,
                Direccion = nuevaDireccion,
                Telefono = nuevoTelefono
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
                AnsiConsole.MarkupLine($"[green]Distribuidor actualizado con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al actualizar el distribuidor.[/]");
        }
    }
} 