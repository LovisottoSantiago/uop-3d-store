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
    public static class RealizarOrdenMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
        {
            var fecha = DateTime.Now;
            var estados = _context.Estados.ToList();
            var estadoPendiente = estados.FirstOrDefault(e => e.NombreEstado.ToLower().Contains("pendiente")) ?? estados.First();

            var detalles = new List<OrdenDeCompraDetalle>();
            var agregarMas = true;
            while (agregarMas)
            {
                var filamentos = _context.Filamentos.Where(f => f.Estado && f.Stock > 0).ToList();
                var insumos = _context.Insumos.Where(i => i.Estado && i.Stock > 0).ToList();
                var productos = filamentos.Cast<Producto>().Concat(insumos.Cast<Producto>()).ToList();
                if (!productos.Any())
                {
                    AnsiConsole.MarkupLine("[red]No hay productos disponibles para la venta.[/]");
                    return;
                }
                var producto = AnsiConsole.Prompt(
                    new SelectionPrompt<Producto>()
                        .Title("Seleccione el producto:")
                        .AddChoices(productos)
                        .UseConverter(p => $"{p.Id} - {p.Nombre} (Stock: {p.Stock})")
                );
                var cantidad = AnsiConsole.Ask<int>("Cantidad:", 1);
                if (cantidad > producto.Stock)
                {
                    AnsiConsole.MarkupLine($"[red]Stock insuficiente. Stock disponible: {producto.Stock}[/]");
                    continue;
                }
                detalles.Add(new OrdenDeCompraDetalle
                {
                    ProductoId = producto.Id,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                });
                agregarMas = AnsiConsole.Confirm("¿Agregar otro producto?", false);
            }

            if (!detalles.Any())
            {
                AnsiConsole.MarkupLine("[red]No se agregaron productos a la orden.[/]");
                return;
            }

            var command = new Application.Commands.OrdenDeCompraCommands.Create.CreateOrdenDeCompraCommand
            {
                Fecha = fecha,
                EstadoId = estadoPendiente.Id,
                Detalles = detalles
            };
            var orden = await mediator.Send(command);
            if (orden != null)
            {
                AnsiConsole.MarkupLine($"[green]Orden creada con éxito! ID: {orden.Id}[/]");
                // Crear cobranza automáticamente
                var estadoCobranzaPendiente = estados.FirstOrDefault(e => e.NombreEstado.ToLower().Contains("pendiente")) ?? estados.First();
                var cobrarCommand = new Application.Commands.CobranzaCommands.Cobrar.CobrarCommand
                {
                    OrdenDeCompraId = orden.Id,
                    FechaPago = fecha,
                    EstadoId = estadoCobranzaPendiente.Id
                };
                var cobranza = await mediator.Send(cobrarCommand);
                if (cobranza != null)
                {
                    AnsiConsole.MarkupLine($"[green]Cobranza creada automáticamente! ID: {cobranza.Id}[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Error al crear la cobranza automáticamente.[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al crear la orden de compra.[/]");
            }
        }
    }
} 