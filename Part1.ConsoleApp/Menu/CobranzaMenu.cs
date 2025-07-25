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
                    case CobranzaOpciones.Editar:
                        await EditarEstadoCobranza(mediator, _context);
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
            Editar,
            Volver
        }

        private static async Task AgregarCobranza(IMediator mediator, AppDbContext _context)
        {
            var ordenes = _context.OrdenDeCompras.ToList();
            
            var orden = AnsiConsole.Prompt(
                new SelectionPrompt<OrdenDeCompra>()
                    .Title("Seleccione la orden de compra:")
                    .AddChoices(ordenes)
                    .UseConverter(o => $"{o.Id} - {o.Fecha.ToString("dd-MM-yyyy")}")
            );

            var fechaPago = AnsiConsole.Ask<DateTime>("Fecha de pago (dd-MM-yyyy):");
            var estados = _context.Estados.ToList();
            var estado = AnsiConsole.Prompt(
                new SelectionPrompt<Estado>()
                    .Title("Seleccione el estado:")
                    .AddChoices(estados)
                    .UseConverter(e => $"{e.Id} - {e.NombreEstado}")
            );
            
            var command = new Application.Commands.CobranzaCommands.Cobrar.CobrarCommand
            {
                OrdenDeCompraId = orden.Id,
                FechaPago = fechaPago,
                EstadoId = estado.Id
            };

            var resultado = await mediator.Send(command);
            
            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Cobranza creada con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al crear la cobranza.[/]");
            }
        }

        private static async Task ListarCobranzas(IMediator mediator)
        {
            var cobranzas = await mediator.Send(new Application.Queries.CobranzaQueries.Get.GetAllCobranzasQuery());
            var table = new Table().AddColumn("ID").AddColumn("Orden").AddColumn("Fecha Pago").AddColumn("Monto").AddColumn("Estado");
            
            foreach (var cobranza in cobranzas)
            {
                table.AddRow(cobranza.Id.ToString(), cobranza.OrdenDeCompraId.ToString(), cobranza.FechaPago.ToString("dd-MM-yyyy"), cobranza.MontoPagado.ToString(), cobranza.Estado?.NombreEstado ?? "");
            }

            AnsiConsole.Write(table);
        }

        private static async Task EditarEstadoCobranza(IMediator mediator, AppDbContext _context)
        {
            var cobranzas = _context.Cobranzas.ToList();

            if (!cobranzas.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay cobranzas para editar.[/]");
                return;
            }

            var cobranza = AnsiConsole.Prompt(
                new SelectionPrompt<Cobranza>()
                    .Title("Seleccione la cobranza a editar:")
                    .AddChoices(cobranzas)
                    .UseConverter(c => $"{c.Id} - {c.FechaPago:dd-MM-yyyy} - {c.MontoPagado}")
            );

            // Permitir editar la fecha de pago también
            var nuevaFechaPago = AnsiConsole.Ask<DateTime>("Nueva fecha de pago (dd-MM-yyyy):", cobranza.FechaPago);

            var estados = _context.Estados.ToList();
            var estado = AnsiConsole.Prompt(
                new SelectionPrompt<Estado>()
                    .Title("Seleccione el nuevo estado:")
                    .AddChoices(estados)
                    .UseConverter(e => $"{e.Id} - {e.NombreEstado}")
            );

            var command = new Application.Commands.CobranzaCommands.Cobrar.CobrarCommand
            {
                CobranzaId = cobranza.Id,
                OrdenDeCompraId = cobranza.OrdenDeCompraId,
                FechaPago = nuevaFechaPago,
                EstadoId = estado.Id
            };

            var resultado = await mediator.Send(command);

            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Estado de la cobranza actualizado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al actualizar el estado de la cobranza.[/]");
            }
        }


    }
} 