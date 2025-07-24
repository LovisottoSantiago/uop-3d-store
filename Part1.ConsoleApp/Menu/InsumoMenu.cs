using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class InsumoMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<InsumoOpciones>()
                        .Title("\n[blue]Insumos:[/]")
                        .AddChoices(Enum.GetValues<InsumoOpciones>())
                );

                switch (opcion)
                {
                    case InsumoOpciones.Agregar:
                        await AgregarInsumo(mediator, _context);
                        break;
                    case InsumoOpciones.Listar:
                        await ListarInsumos(mediator);
                        break;
                    case InsumoOpciones.Volver:
                        return;
                }
            }
        }

        enum InsumoOpciones
        {
            Agregar,
            Listar,
            Volver
        }


        private static async Task AgregarInsumo(IMediator mediator, AppDbContext _context)
        {
            Console.WriteLine("\n[green]Agregar Insumo:");
            var nombre = AnsiConsole.Ask<string>("Nombre:");
            var precio = AnsiConsole.Ask<decimal>("Precio:");
            var stock = AnsiConsole.Ask<int>("Stock:");
            var estado = AnsiConsole.Confirm("¿Está activo?", true);
            var color = AnsiConsole.Ask<string>("Color:");
            var imagen = AnsiConsole.Ask<string>("Url de la imagen:");

            var marcas = _context.Marcas.ToList();
            var marca = AnsiConsole.Prompt(
                new SelectionPrompt<Marca>()
                    .Title("Seleccione la marca:")
                    .AddChoices(marcas)
                    .UseConverter(m => $"{m.Id} - {m.Nombre}")
            );

            var distribuidores = _context.Distribuidores.ToList();
            var distribuidor = AnsiConsole.Prompt(
                new SelectionPrompt<Distribuidor>()
                    .Title("Seleccione el distribuidor:")
                    .AddChoices(distribuidores)
                    .UseConverter(d => $"{d.Id} - {d.Nombre}")
            );

            var command = new Application.Commands.InsumoCommands.Create.CreateInsumoCommand
            {
                Nombre = nombre,
                Precio = precio,
                Stock = stock,
                Estado = estado,
                Color = color,
                MarcaId = marca.Id,
                DistribuidorId = distribuidor.Id,
                ImagenUrl = imagen
            };

            var resultado = await mediator.Send(command);

            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Insumo creado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al crear el insumo.[/]");
            }
        }

        private static async Task ListarInsumos(IMediator mediator)
        {
            var insumos = await mediator.Send(new Application.Queries.InsumoQueries.Get.GetAllInsumosQuery());
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var insumo in insumos)
            {
                table.AddRow(insumo.Id.ToString(), insumo.Nombre);
            }
            AnsiConsole.Write(table);
        }



    }
} 