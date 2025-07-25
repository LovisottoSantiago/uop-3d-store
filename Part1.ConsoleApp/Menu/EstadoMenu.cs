using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Part1.ConsoleApp.Menu
{
    public static class EstadoMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<EstadoOpciones>()
                        .Title("\n[blue]Estados:[/]")
                        .AddChoices(Enum.GetValues<EstadoOpciones>())
                );

                switch (opcion)
                {
                    case EstadoOpciones.Agregar:
                        await AgregarEstado(mediator);
                        break;
                    case EstadoOpciones.Listar:
                        await ListarEstado(mediator);
                        break;
                    case EstadoOpciones.Editar:
                        await EditarEstado(mediator, _context);
                        break;
                    case EstadoOpciones.Volver:
                        return;
                }
            }
        }

        enum EstadoOpciones
        {
            Agregar,
            Listar,
            Editar,
            Volver
        }

        private static async Task AgregarEstado(IMediator mediator)
        {
            var nombre = AnsiConsole.Ask<string>("Nombre del estado:");
            var command = new Application.Commands.EstadoCommands.Create.CreateEstadoCommand
            {
                NombreEstado = nombre
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Estado creado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al crear el estado.[/]");
            }
        }

        private static async Task ListarEstado(IMediator mediator)
        {
            var estados = await mediator.Send(new Application.Queries.EstadoQueries.Get.GetAllEstadosQuery());
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var estado in estados)
            {
                table.AddRow(estado.Id.ToString(), estado.NombreEstado);
            }
            AnsiConsole.Write(table);
        }

        private static async Task EditarEstado(IMediator mediator, AppDbContext _context)
        {
            var estados = await mediator.Send(new Application.Queries.EstadoQueries.Get.GetAllEstadosQuery());
            if (estados == null || !estados.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay estados para editar.[/]");
                return;
            }

            var estado = AnsiConsole.Prompt(
                new SelectionPrompt<Estado>()
                    .Title("Seleccione el estado a editar:")
                    .AddChoices(estados)
                    .UseConverter(e => $"{e.Id} - {e.NombreEstado}")
            );

            var estadoActual = await mediator.Send(new Application.Queries.EstadoQueries.Get.GetEstadoByIdQuery { Id = estado.Id });
            if (estadoActual == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró el estado seleccionado.[/]");
                return;
            }

            var nuevoNombre = AnsiConsole.Ask<string>("Nombre del estado:", estadoActual.NombreEstado);

            var command = new Application.Commands.EstadoCommands.Update.UpdateEstadoCommand
            {
                Id = estadoActual.Id,
                NombreEstado = nuevoNombre
            };

            var resultado = await mediator.Send(command);
            
            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Estado editado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al editar el estado.[/]");
            }
        }

        private static async Task EliminarEstado(IMediator mediator, AppDbContext _context)
        {
            var estados = await mediator.Send(new Application.Queries.EstadoQueries.Get.GetAllEstadosQuery());
            if (estados == null || !estados.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay estados para eliminar.[/]");
                return;
            }

            var estado = AnsiConsole.Prompt(
                new SelectionPrompt<Estado>()
                    .Title("Seleccione el estado a eliminar:")
                    .AddChoices(estados)
                    .UseConverter(e => $"{e.Id} - {e.NombreEstado}")
            );

            var estadoActual = await mediator.Send(new Application.Queries.EstadoQueries.Get.GetEstadoByIdQuery { Id = estado.Id });
            if (estadoActual == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró el estado seleccionado.[/]");
                return;
            }

            var command = new Application.Commands.EstadoCommands.Delete.DeleteEstadoCommand
            {
                Id = estadoActual.Id
            };

            var resultado = await mediator.Send(command);

            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Estado eliminado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al eliminar el estado.[/]");
            }
        }

    }
} 