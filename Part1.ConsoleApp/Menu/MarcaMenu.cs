using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class MarcaMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<MarcaOpciones>()
                        .Title("\n[blue]Marcas:[/]")
                        .AddChoices(Enum.GetValues<MarcaOpciones>())
                );

                switch (opcion)
                {
                    case MarcaOpciones.Agregar:
                        await AgregarMarca(mediator, _context);
                        break;
                    case MarcaOpciones.Listar:
                        await ListarMarcas(mediator);
                        break;
                    case MarcaOpciones.Editar:
                        await EditarMarcas(mediator, _context);
                        break;
                    case MarcaOpciones.Volver:
                        return;
                }
            }
        }

        enum MarcaOpciones
        {
            Agregar,
            Listar,
            Editar,
            Volver
        }

        private static async Task AgregarMarca(IMediator mediator, AppDbContext _context)
        {
            var nombre = AnsiConsole.Ask<string>("Nombre de la marca:");
            var distribuidores = _context.Distribuidores.ToList();

            var distribuidoresSeleccionados = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Distribuidor>()
                    .Title("Seleccione los distribuidores (puede elegir varios):")
                    .NotRequired()
                    .AddChoices(distribuidores)
                    .UseConverter(d => $"{d.Id} - {d.Nombre}")
            );

            var command = new Application.Commands.MarcaCommands.Create.CreateMarcaCommand
            {
                Nombre = nombre,
                DistribuidorIds = distribuidoresSeleccionados.Select(d => d.Id).ToList()
            };
            var resultado = await mediator.Send(command);

            if (resultado != null)
                AnsiConsole.MarkupLine($"[green]Marca creada con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al crear la marca.[/]");
        }

        private static async Task ListarMarcas(IMediator mediator)
        {
            var marcas = await mediator.Send(new Application.Queries.MarcaQueries.Get.GetAllMarcasQuery());
            var table = new Table().AddColumn("ID").AddColumn("Nombre").AddColumn("Distribuidor");
            foreach (var marca in marcas)
            {
                var nombresDistribuidores = marca.DistribuidorMarcas?
                    .Select(dm => dm.Distribuidor.Nombre)
                    .Where(nombre => !string.IsNullOrEmpty(nombre))
                    .ToList();

                var distribuidoresTexto = nombresDistribuidores != null && nombresDistribuidores.Any()
                    ? string.Join(", ", nombresDistribuidores)
                    : "Sin distribuidores";

                table.AddRow(marca.Id.ToString(), marca.Nombre, distribuidoresTexto);
            }
            AnsiConsole.Write(table);
        }

        private static async Task EditarMarcas(IMediator mediator, AppDbContext _context)
        {
            var marcas = await mediator.Send(new Application.Queries.MarcaQueries.Get.GetAllMarcasQuery());
            if (marcas == null || !marcas.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay marcas para editar.[/]");
                return;
            }

            var marca = AnsiConsole.Prompt(
                new SelectionPrompt<Marca>()
                    .Title("Seleccione la marca a editar:")
                    .AddChoices(marcas)
                    .UseConverter(m => $"{m.Id} - {m.Nombre}")
            );

            var marcaActual = await mediator.Send(new Application.Queries.MarcaQueries.Get.GetMarcaByIdQuery { Id = marca.Id });
            if (marcaActual == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró la marca seleccionada.[/]");
                return;
            }

            var nuevoNombre = AnsiConsole.Ask<string>("Nombre de la marca:", marcaActual.Nombre);

            var distribuidores = _context.Distribuidores.ToList();
            if (!distribuidores.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay distribuidores disponibles.[/]");
                return;
            }

            var preseleccionados = marcaActual.DistribuidorMarcas?
                .Select(dm => distribuidores.FirstOrDefault(d => d.Id == dm.DistribuidorId))
                .Where(d => d != null)
                .ToArray();

            var prompt = new MultiSelectionPrompt<Distribuidor>()
                .Title("Seleccione los distribuidores (puede elegir varios):")
                .NotRequired()
                .AddChoices(distribuidores)
                .UseConverter(d => $"{d.Id} - {d.Nombre}");

            if (preseleccionados != null && preseleccionados.Length > 0)
            {
                foreach (var d in preseleccionados)
                    prompt.Select(d);
            }

            var distribuidoresSeleccionados = AnsiConsole.Prompt(prompt);

            var command = new Application.Commands.MarcaCommands.Update.UpdateMarcaCommand
            {
                Id = marcaActual.Id,
                Nombre = nuevoNombre,
                DistribuidorIds = distribuidoresSeleccionados.Select(d => d.Id).ToList()
            };

            var resultado = await mediator.Send(command);

            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Marca editada con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al editar la marca.[/]");
            }
        }

        private static async Task EliminarMarcas(IMediator mediator, AppDbContext _context)
        {
            var marcas = await mediator.Send(new Application.Queries.MarcaQueries.Get.GetAllMarcasQuery());
            if (marcas == null || !marcas.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay marcas para eliminar.[/]");
                return;
            }

            var marca = AnsiConsole.Prompt(
                new SelectionPrompt<Marca>()
                    .Title("Seleccione la marca a eliminar:")
                    .AddChoices(marcas)
                    .UseConverter(m => $"{m.Id} - {m.Nombre}")
            );

            var marcaActual = await mediator.Send(new Application.Queries.MarcaQueries.Get.GetMarcaByIdQuery { Id = marca.Id });
            if (marcaActual == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró la marca seleccionada.[/]");
                return;
            }

            var command = new Application.Commands.MarcaCommands.Delete.DeleteMarcaCommand
            {
                Id = marcaActual.Id
            };

            var resultado = await mediator.Send(command);

            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Marca eliminada con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al eliminar la marca.[/]");
            }
        }


    }
} 