using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System.Linq;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class OrdenDeCompraDetalleMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<OrdenDeCompraDetalleOpciones>()
                        .Title("\n[blue]Detalles de Orden de Compra:[/]")
                        .AddChoices(Enum.GetValues<OrdenDeCompraDetalleOpciones>())
                );

                switch (opcion)
                {
                    case OrdenDeCompraDetalleOpciones.Agregar:
                        await AgregarDetalle(mediator, _context);
                        break;
                    case OrdenDeCompraDetalleOpciones.Listar:
                        await ListarDetalles(mediator);
                        break;
                    case OrdenDeCompraDetalleOpciones.Volver:
                        return;
                }
            }
        }

        enum OrdenDeCompraDetalleOpciones
        {
            Agregar,
            Listar,
            Volver
        }

        private static async Task AgregarDetalle(IMediator mediator, AppDbContext _context)
        {
            var ordenes = _context.OrdenDeCompras.ToList();
            var orden = AnsiConsole.Prompt(
                new SelectionPrompt<OrdenDeCompra>()
                    .Title("Seleccione la orden de compra:")
                    .AddChoices(ordenes)
                    .UseConverter(o => $"{o.Id} - {o.Fecha.ToString("dd-MM-yyyy")}")
            );
            var productos = _context.Filamentos.Cast<Producto>().Concat(_context.Insumos).ToList();
            var producto = AnsiConsole.Prompt(
                new SelectionPrompt<Producto>()
                    .Title("Seleccione el producto:")
                    .AddChoices(productos)
                    .UseConverter(p => $"{p.Id} - {p.Nombre}")
            );
            var cantidad = AnsiConsole.Ask<int>("Cantidad:");
            var precioUnitario = AnsiConsole.Ask<decimal>("Precio unitario:");
            var command = new Application.Commands.OrdenDeCompraDetalleCommands.Create.CreateOrdenDeCompraDetalleCommand
            {
                OrdenDeCompraId = orden.Id,
                ProductoId = producto.Id,
                Cantidad = cantidad,
                PrecioUnitario = precioUnitario
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
                AnsiConsole.MarkupLine($"[green]Detalle creado con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al crear el detalle.[/]");
        }

        private static async Task ListarDetalles(IMediator mediator)
        {
            var detalles = await mediator.Send(new Application.Queries.OrdenDeCompraDetalleQueries.Get.GetAllOrdenesDeCompraDetalleQuery());
            var table = new Table().AddColumn("ID").AddColumn("Orden").AddColumn("Producto").AddColumn("Cantidad").AddColumn("Precio Unitario");
            foreach (var detalle in detalles)
            {
                table.AddRow(detalle.Id.ToString(), detalle.OrdenDeCompraId.ToString(), detalle.ProductoId.ToString(), detalle.Cantidad.ToString(), detalle.PrecioUnitario.ToString());
            }
            AnsiConsole.Write(table);
        }
    }
} 