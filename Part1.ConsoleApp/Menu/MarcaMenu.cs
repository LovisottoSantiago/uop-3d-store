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
                    case MarcaOpciones.Volver:
                        return;
                }
            }
        }

        enum MarcaOpciones
        {
            Agregar,
            Listar,
            Volver
        }

        private static async Task AgregarMarca(IMediator mediator, AppDbContext _context)
        {
            var nombre = AnsiConsole.Ask<string>("Nombre de la marca:");
            var distribuidores = _context.Distribuidores.ToList();
            var distribuidor = AnsiConsole.Prompt(
                new SelectionPrompt<Distribuidor>()
                    .Title("Seleccione el distribuidor:")
                    .AddChoices(distribuidores)
                    .UseConverter(d => $"{d.Id} - {d.Nombre}")
            );

            var command = new Application.Commands.MarcaCommands.Create.CreateMarcaCommand
            {
                Nombre = nombre,
                DistribuidorId = distribuidor.Id
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
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var marca in marcas)
            {
                table.AddRow(marca.Id.ToString(), marca.Nombre);
            }
            AnsiConsole.Write(table);
        }
    }
} 