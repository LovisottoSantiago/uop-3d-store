using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class OrdenDeCompraMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<OrdenDeCompraOpciones>()
                        .Title("\n[blue]Órdenes de Compra:[/]")
                        .AddChoices(Enum.GetValues<OrdenDeCompraOpciones>())
                );

                switch (opcion)
                {
                    case OrdenDeCompraOpciones.Agregar:
                        await AgregarOrdenDeCompra(mediator, _context);
                        break;
                    case OrdenDeCompraOpciones.Listar:
                        await ListarOrdenesDeCompra(mediator);
                        break;
                    case OrdenDeCompraOpciones.Volver:
                        return;
                }
            }
        }

        enum OrdenDeCompraOpciones
        {
            Agregar,
            Listar,
            Volver
        }

        private static async Task AgregarOrdenDeCompra(IMediator mediator, AppDbContext _context)
        {
            var fecha = AnsiConsole.Ask<DateTime>("Fecha de la orden (yyyy-MM-dd):");
            var estados = _context.Estados.ToList();
            var estado = AnsiConsole.Prompt(
                new SelectionPrompt<Estado>()
                    .Title("Seleccione el estado:")
                    .AddChoices(estados)
                    .UseConverter(e => $"{e.Id} - {e.NombreEstado}")
            );

            // Detalles
            var detalles = new List<OrdenDeCompraDetalle>();
            var agregarMas = true;
            while (agregarMas)
            {
                var productos = _context.Filamentos.Cast<Producto>().Concat(_context.Insumos).ToList();
                var producto = AnsiConsole.Prompt(
                    new SelectionPrompt<Producto>()
                        .Title("Seleccione el producto:")
                        .AddChoices(productos)
                        .UseConverter(p => $"{p.Id} - {p.Nombre}")
                );
                var cantidad = AnsiConsole.Ask<int>("Cantidad:");
                var precioUnitario = AnsiConsole.Ask<decimal>("Precio unitario:");
                detalles.Add(new OrdenDeCompraDetalle
                {
                    ProductoId = producto.Id,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario
                });
                agregarMas = AnsiConsole.Confirm("¿Agregar otro producto?", false);
            }

            var command = new Application.Commands.OrdenDeCompraCommands.Create.CreateOrdenDeCompraCommand
            {
                Fecha = fecha,
                EstadoId = estado.Id,
                Detalles = detalles
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
                AnsiConsole.MarkupLine($"[green]Orden de compra creada con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al crear la orden de compra.[/]");
        }

        private static async Task ListarOrdenesDeCompra(IMediator mediator)
        {
            var ordenes = await mediator.Send(new Application.Queries.OrdenDeCompraQueries.Get.GetAllOrdenesDeCompraQuery());
            var table = new Table().AddColumn("ID").AddColumn("Fecha").AddColumn("Estado");
            foreach (var orden in ordenes)
            {
                table.AddRow(orden.Id.ToString(), orden.Fecha.ToShortDateString(), orden.Estado?.NombreEstado ?? "");
            }
            AnsiConsole.Write(table);
        }
    }
} 