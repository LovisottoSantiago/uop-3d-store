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
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext dbContext)
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