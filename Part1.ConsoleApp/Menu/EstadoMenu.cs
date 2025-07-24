using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System.Threading.Tasks;

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
                        await ListarEstados(mediator);
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
                AnsiConsole.MarkupLine($"[green]Estado creado con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al crear el estado.[/]");
        }

        private static async Task ListarEstados(IMediator mediator)
        {
            var estados = await mediator.Send(new Application.Queries.EstadoQueries.Get.GetAllEstadosQuery());
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var estado in estados)
            {
                table.AddRow(estado.Id.ToString(), estado.NombreEstado);
            }
            AnsiConsole.Write(table);
        }
    }
} 