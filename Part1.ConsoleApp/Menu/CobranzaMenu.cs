using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class CobranzaMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext _context)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<CobranzaOpciones>()
                        .Title("\n[blue]Cobranzas:[/]")
                        .AddChoices(Enum.GetValues<CobranzaOpciones>())
                );

                switch (opcion)
                {
                    case CobranzaOpciones.Agregar:
                        await AgregarCobranza(mediator, _context);
                        break;
                    case CobranzaOpciones.Listar:
                        await ListarCobranzas(mediator);
                        break;
                    case CobranzaOpciones.Volver:
                        return;
                }
            }
        }

        enum CobranzaOpciones
        {
            Agregar,
            Listar,
            Volver
        }

        private static async Task AgregarCobranza(IMediator mediator, AppDbContext _context)
        {
            var ordenes = _context.OrdenDeCompras.ToList();
            var orden = AnsiConsole.Prompt(
                new SelectionPrompt<OrdenDeCompra>()
                    .Title("Seleccione la orden de compra:")
                    .AddChoices(ordenes)
                    .UseConverter(o => $"{o.Id} - {o.Fecha.ToShortDateString()}")
            );
            var fechaPago = AnsiConsole.Ask<DateTime>("Fecha de pago (yyyy-MM-dd):");
            var montoPagado = AnsiConsole.Ask<decimal>("Monto pagado:");
            var estados = _context.Estados.ToList();
            var estado = AnsiConsole.Prompt(
                new SelectionPrompt<Estado>()
                    .Title("Seleccione el estado:")
                    .AddChoices(estados)
                    .UseConverter(e => $"{e.Id} - {e.NombreEstado}")
            );
            var command = new Application.Commands.CobranzaCommands.Create.CreateCobranzaCommand
            {
                OrdenDeCompraId = orden.Id,
                FechaPago = fechaPago,
                MontoPagado = montoPagado,
                EstadoId = estado.Id
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
                AnsiConsole.MarkupLine($"[green]Cobranza creada con éxito! ID: {resultado.Id}[/]");
            else
                AnsiConsole.MarkupLine("[red]Error al crear la cobranza.[/]");
        }

        private static async Task ListarCobranzas(IMediator mediator)
        {
            var cobranzas = await mediator.Send(new Application.Queries.CobranzaQueries.Get.GetAllCobranzasQuery());
            var table = new Table().AddColumn("ID").AddColumn("Orden").AddColumn("Fecha Pago").AddColumn("Monto").AddColumn("Estado");
            foreach (var cobranza in cobranzas)
            {
                table.AddRow(cobranza.Id.ToString(), cobranza.OrdenDeCompraId.ToString(), cobranza.FechaPago.ToShortDateString(), cobranza.MontoPagado.ToString(), cobranza.Estado?.NombreEstado ?? "");
            }
            AnsiConsole.Write(table);
        }
    }
} 