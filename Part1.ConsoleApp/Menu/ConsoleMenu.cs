using MediatR;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class ConsoleMenu
    {
        public static async Task MostrarMenu(IMediator mediator, AppDbContext _context)
        {
            while (true)
            {
                var opcion = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOpciones>()
                        .Title("\n[green]Menu de opciones:[/]")
                        .AddChoices(Enum.GetValues<MenuOpciones>())
                );

                switch (opcion)
                {
                    case MenuOpciones.Filamentos:
                        await FilamentoMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.Insumos:
                        await InsumoMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.Marcas:
                        await MarcaMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.Distribuidores:
                        await DistribuidorMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.TipoMateriales:
                        await TipoMaterialMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.Estados:
                        await EstadoMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.OrdenesDeCompra:
                        await OrdenDeCompraMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.OrdenesDeCompraDetalle:
                        await OrdenDeCompraDetalleMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.Cobranzas:
                        await CobranzaMenu.MostrarSubMenu(mediator, _context);
                        break;
                    case MenuOpciones.Salir:
                        Console.WriteLine("Saliendo...");
                        return;
                }
            }
        }

        enum MenuOpciones
        {
            Filamentos,
            Insumos,
            Marcas,
            Distribuidores,
            TipoMateriales,
            Estados,
            OrdenesDeCompra,
            OrdenesDeCompraDetalle,
            Cobranzas,
            Salir
        }
    }
} 