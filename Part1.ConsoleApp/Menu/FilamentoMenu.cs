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
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
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
                        await AgregarFilamento(mediator, _context);
                        break;
                    case FilamentoOpciones.Listar:
                        await ListarFilamentos(mediator);
                        break;
                    case FilamentoOpciones.Volver:
                        return;
                }
            }
        }

        enum FilamentoOpciones
        {
            Agregar,
            Listar,
            Volver
        }

        private static async Task AgregarFilamento(IMediator mediator, AppDbContext _context)
        {
            Console.WriteLine("\n[green]Agregar Filamento:");
            var nombre = AnsiConsole.Ask<string>("Nombre:");
            var precio = AnsiConsole.Ask<decimal>("Precio:");
            var peso = AnsiConsole.Ask<float>("Peso:");
            var stock = AnsiConsole.Ask<int>("Stock:");
            var estado = AnsiConsole.Confirm("¿Está activo?", true);
            var color = AnsiConsole.Ask<string>("Color:");
            var imagen = AnsiConsole.Ask<string>("Url de la imagen:");

            var tipos = _context.TipoMateriales.ToList();
            var tipoMaterial = AnsiConsole.Prompt(
                new SelectionPrompt<TipoMaterial>()
                    .Title("Seleccione el tipo de material:")
                    .AddChoices(tipos)
                    .UseConverter(t => $"{t.Id} - {t.Nombre}")
            );

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
                DistribuidorId = distribuidor.Id,
                ImagenUrl = imagen
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

        private static async Task ListarFilamentos(IMediator mediator)
        {
            var filamentos = await mediator.Send(new Application.Queries.FilamentoQueries.Get.GetAllFilamentosQuery());
            var table = new Table().AddColumn("ID").AddColumn("Nombre");
            foreach (var filamento in filamentos)
            {
                table.AddRow(filamento.Id.ToString(), filamento.Nombre);
            }
            AnsiConsole.Write(table);
        }

    }
} 