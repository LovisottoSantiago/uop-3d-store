using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class TipoMaterialMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext dbContext)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<TipoMaterialOpciones>()
                        .Title("\n[blue]Tipos de Material:[/]")
                        .AddChoices(Enum.GetValues<TipoMaterialOpciones>())
                );

                switch (opcion)
                {
                    case TipoMaterialOpciones.Agregar:
                        await AgregarTipoMaterial(mediator);
                        break;
                    case TipoMaterialOpciones.Listar:
                        await ListarTipoMateriales(mediator);
                        break;
                    case TipoMaterialOpciones.Volver:
                        return;
                }
            }
        }

        enum TipoMaterialOpciones
        {
            Agregar,
            Listar,
            Volver
        }

        private static async Task AgregarTipoMaterial(IMediator mediator)
        {
            var nombre = AnsiConsole.Ask<string>("Nombre del tipo de material:");
            var command = new Application.Commands.TipoMaterialCommands.Create.CreateTipoMaterialCommand
            {
                Nombre = nombre
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
                AnsiConsole.MarkupLine($"[green]Tipo de material creado con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al crear el tipo de material.[/]");
        }

        private static async Task ListarTipoMateriales(IMediator mediator)
        {
            var tipos = await mediator.Send(new Application.Queries.TipoMaterialQueries.Get.GetAllTipoMaterialesQuery());
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var tipo in tipos)
            {
                table.AddRow(tipo.Id.ToString(), tipo.Nombre);
            }
            AnsiConsole.Write(table);
        }
    }
} 