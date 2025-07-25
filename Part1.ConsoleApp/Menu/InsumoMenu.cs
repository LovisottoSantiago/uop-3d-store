using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Threading.Tasks;
using System.Linq;

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
                    case InsumoOpciones.Editar:
                        await EditarInsumo(mediator, _context);
                        break;
                    case InsumoOpciones.Eliminar:
                        await EliminarInsumo(mediator, _context);
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
            Editar,
            Eliminar,
            Volver
        }


        private static async Task AgregarInsumo(IMediator mediator, AppDbContext _context)
        {
            Console.WriteLine("\n[green]Agregar Insumo:");
            var nombre = AnsiConsole.Ask<string>("Nombre:");
            var precio = AnsiConsole.Ask<decimal>("Precio:");
            var stock = AnsiConsole.Ask<int>("Stock:");
            var estado = true;
            var color = AnsiConsole.Ask<string>("Color:");
            var imagen = AnsiConsole.Ask<string>("Url de la imagen:");

            var marcas = _context.Marcas.ToList();
            var marca = AnsiConsole.Prompt(
                new SelectionPrompt<Marca>()
                    .Title("Seleccione la marca:")
                    .AddChoices(marcas)
                    .UseConverter(m => $"{m.Id} - {m.Nombre}")
            );

            var command = new Application.Commands.InsumoCommands.Create.CreateInsumoCommand
            {
                Nombre = nombre,
                Precio = precio,
                Stock = stock,
                Estado = estado,
                Color = color,
                MarcaId = marca.Id,
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
            var insumosHabilitados = insumos.Where(i => i.Estado).ToList();
            var table = new Table().AddColumn("ID").AddColumn("Nombre").AddColumn("Precio").AddColumn("Stock");
            foreach (var insumo in insumosHabilitados)
            {
                table.AddRow(insumo.Id.ToString(), insumo.Nombre, insumo.Precio.ToString(), insumo.Stock.ToString());
            }
            AnsiConsole.Write(table);
        }

        private static async Task EditarInsumo(IMediator mediator, AppDbContext _context)
        {
            var insumos = await mediator.Send(new Application.Queries.InsumoQueries.Get.GetAllInsumosQuery());
            var insumosHabilitados = insumos.Where(i => i.Estado).ToList();
            if (!insumosHabilitados.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay insumos para editar.[/]");
                return;
            }
            var insumo = AnsiConsole.Prompt(
                new SelectionPrompt<Insumo>()
                    .Title("Seleccione el insumo a editar:")
                    .AddChoices(insumosHabilitados)
                    .UseConverter(i => $"{i.Id} - {i.Nombre}")
            );
            var insumoActual = insumo;
            var nuevoPrecio = AnsiConsole.Ask<decimal>("Precio:", insumoActual.Precio);
            var nuevoStock = AnsiConsole.Ask<int>("Stock:", insumoActual.Stock);
            var nuevoEstado = AnsiConsole.Confirm("¿Está habilitado?", insumoActual.Estado);
            var nuevaImagen = AnsiConsole.Ask<string>("Url de la imagen:", insumoActual.ImagenUrl);
            var nuevoColor = AnsiConsole.Ask<string>("Color:", insumoActual.Color);

            var marcas = _context.Marcas.ToList();
            var marca = AnsiConsole.Prompt(
                new SelectionPrompt<Marca>()
                    .Title("Seleccione la marca:")
                    .AddChoices(marcas)
                    .UseConverter(m => $"{m.Id} - {m.Nombre}")
            );

            var nombreGenerado = $"{marca.Nombre.ToUpper()} {nuevoColor.ToUpper()}";
            var usarNombreGenerado = AnsiConsole.Confirm($"¿Deseás usar el nombre generado automáticamente: [yellow]{nombreGenerado}[/]?", true);
            var nombreFinal = usarNombreGenerado
                ? nombreGenerado
                : AnsiConsole.Ask<string>("Ingresá un nombre personalizado para el insumo:");

            var command = new Application.Commands.InsumoCommands.Update.UpdateInsumoCommand
            {
                Id = insumoActual.Id,
                Precio = nuevoPrecio,
                Stock = nuevoStock,
                Estado = nuevoEstado,
                ImagenUrl = nuevaImagen,
                MarcaId = marca.Id,
                Color = nuevoColor,
                Nombre = nombreFinal
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Insumo editado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al editar el insumo.[/]");
            }
        }

        private static async Task EliminarInsumo(IMediator mediator, AppDbContext _context)
        {
            var insumos = await mediator.Send(new Application.Queries.InsumoQueries.Get.GetAllInsumosQuery());
            var insumosHabilitados = insumos.Where(i => i.Estado).ToList();
            if (!insumosHabilitados.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay insumos habilitados para eliminar.[/]");
                return;
            }
            var insumo = AnsiConsole.Prompt(
                new SelectionPrompt<Insumo>()
                    .Title("Seleccione el insumo a eliminar (deshabilitar):")
                    .AddChoices(insumosHabilitados)
                    .UseConverter(i => $"{i.Id} - {i.Nombre}")
            );
            var confirm = AnsiConsole.Confirm($"¿Está seguro que desea deshabilitar el insumo '{insumo.Nombre}'?");
            if (!confirm)
                return;
            var command = new Application.Commands.InsumoCommands.Update.UpdateInsumoCommand
            {
                Id = insumo.Id,
                Precio = insumo.Precio,
                Stock = insumo.Stock,
                Estado = false,
                ImagenUrl = insumo.ImagenUrl,
                MarcaId = insumo.MarcaId,
                Color = insumo.Color,
                Nombre = insumo.Nombre
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Insumo deshabilitado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al deshabilitar el insumo.[/]");
            }
        }


    }
} 