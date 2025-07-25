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
                    case FilamentoOpciones.Editar:
                        await EditarFilamento(mediator, _context);
                        break;
                    case FilamentoOpciones.Eliminar:
                        await EliminarFilamento(mediator, _context);
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
            Editar,
            Eliminar,
            Volver
        }

        private static async Task AgregarFilamento(IMediator mediator, AppDbContext _context)
        {
            Console.WriteLine("\n[green]Agregar Filamento:");
            var precio = AnsiConsole.Ask<decimal>("Precio:");
            var peso = AnsiConsole.Ask<float>("Peso (KG):");
            var stock = AnsiConsole.Ask<int>("Stock:");
            var estado = true;
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

            var nombreGenerado = $"{peso}KG {marca.Nombre.ToUpper()} {tipoMaterial.Nombre.ToUpper()} {color.ToUpper()}";
            var usarNombreGenerado = AnsiConsole.Confirm($"¿Deseás usar el nombre generado automáticamente: [yellow]{nombreGenerado}[/]?", true);

            var nombreFinal = usarNombreGenerado
                ? nombreGenerado
                : AnsiConsole.Ask<string>("Ingresá un nombre personalizado para el filamento:");


            var command = new Application.Commands.FilamentoCommands.Create.CreateFilamentoCommand
            {
                Nombre = nombreFinal,
                Precio = precio,
                Peso = peso,
                Stock = stock,
                Estado = estado,
                Color = color,
                TipoMaterialId = tipoMaterial.Id,
                MarcaId = marca.Id,
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
            var filamentosHabilidatos = filamentos.Where(f => f.Estado).ToList();
            var table = new Table().AddColumn("ID").AddColumn("Nombre").AddColumn("Precio").AddColumn("Stock");
            foreach (var filamento in filamentosHabilidatos)
            {
                table.AddRow(filamento.Id.ToString(), filamento.Nombre, filamento.Precio.ToString(), filamento.Stock.ToString());
            }
            AnsiConsole.Write(table);
        }

        private static async Task EditarFilamento(IMediator mediator, AppDbContext _context)
        {
            var filamentos = await mediator.Send(new Application.Queries.FilamentoQueries.Get.GetAllFilamentosQuery());
            if (filamentos == null || !filamentos.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay filamentos para editar.[/]");
                return;
            }

            var filamento = AnsiConsole.Prompt(
                new SelectionPrompt<Filamento>()
                    .Title("Seleccione el filamento a editar:")
                    .AddChoices(filamentos)
                    .UseConverter(f => $"{f.Id} - {f.Nombre}")
            );

            var filamentoActual = await mediator.Send(new Application.Queries.FilamentoQueries.Get.GetFilamentoByIdQuery { Id = filamento.Id });
            if (filamentoActual == null)
            {
                AnsiConsole.MarkupLine("[red]No se encontró el filamento seleccionado.[/]");
                return;
            }

            var nuevoPrecio = AnsiConsole.Ask<decimal>("Precio:", filamentoActual.Precio);
            var nuevoStock = AnsiConsole.Ask<int>("Stock:", filamentoActual.Stock);
            var nuevoEstado = AnsiConsole.Confirm("¿Está habilitado?", filamentoActual.Estado);
            var nuevaImagen = AnsiConsole.Ask<string>("Url de la imagen:", filamentoActual.ImagenUrl);
            var nuevoColor = AnsiConsole.Ask<string>("Color:", filamentoActual.Color);

            var tipos = _context.TipoMateriales.ToList();
            if (!tipos.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay tipos de material disponibles.[/]");
                return;
            }
            var tipoMaterial = AnsiConsole.Prompt(
                new SelectionPrompt<TipoMaterial>()
                    .Title("Seleccione el tipo de material:")
                    .AddChoices(tipos)
                    .UseConverter(t => $"{t.Id} - {t.Nombre}")
            );

            var marcas = _context.Marcas.ToList();
            if (!marcas.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay marcas disponibles.[/]");
                return;
            }
            var marca = AnsiConsole.Prompt(
                new SelectionPrompt<Marca>()
                    .Title("Seleccione la marca:")
                    .AddChoices(marcas)
                    .UseConverter(m => $"{m.Id} - {m.Nombre}")
            );

            var nombreGenerado = $"{filamento.Peso}KG {marca.Nombre.ToUpper()} {tipoMaterial.Nombre.ToUpper()} {nuevoColor.ToUpper()}";
            var usarNombreGenerado = AnsiConsole.Confirm($"¿Deseás usar el nombre generado automáticamente: [yellow]{nombreGenerado}[/]?", true);
            var nombreFinal = usarNombreGenerado
                ? nombreGenerado
                : AnsiConsole.Ask<string>("Ingresá un nombre personalizado para el filamento:");

            var command = new Application.Commands.FilamentoCommands.Update.UpdateFilamentoCommand
            {
                Id = filamentoActual.Id,
                Precio = nuevoPrecio,
                Stock = nuevoStock,
                Estado = nuevoEstado,
                ImagenUrl = nuevaImagen,
                TipoMaterialId = tipoMaterial.Id,
                MarcaId = marca.Id,
                Peso = filamento.Peso,
                Color = nuevoColor,
                Nombre = nombreFinal
            };

            var resultado = await mediator.Send(command);

            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Filamento editado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al editar el filamento.[/]");
            }
        }

        private static async Task EliminarFilamento(IMediator mediator, AppDbContext _context)
        {
            var filamentos = await mediator.Send(new Application.Queries.FilamentoQueries.Get.GetAllFilamentosQuery());
            var filamentosHabilitados = filamentos.Where(f => f.Estado).ToList();
            if (!filamentosHabilitados.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay filamentos habilitados para eliminar.[/]");
                return;
            }
            var filamento = AnsiConsole.Prompt(
                new SelectionPrompt<Filamento>()
                    .Title("Seleccione el filamento a eliminar (deshabilitar):")
                    .AddChoices(filamentosHabilitados)
                    .UseConverter(f => $"{f.Id} - {f.Nombre}")
            );
            var confirm = AnsiConsole.Confirm($"¿Está seguro que desea deshabilitar el filamento '{filamento.Nombre}'?");
            if (!confirm)
                return;
            var command = new Application.Commands.FilamentoCommands.Update.UpdateFilamentoCommand
            {
                Id = filamento.Id,
                Precio = filamento.Precio,
                Stock = filamento.Stock,
                Estado = false,
                ImagenUrl = filamento.ImagenUrl,
                TipoMaterialId = filamento.TipoMaterialId,
                MarcaId = filamento.MarcaId,
                Peso = filamento.Peso,
                Color = filamento.Color,
                Nombre = filamento.Nombre
            };
            var resultado = await mediator.Send(command);
            if (resultado != null)
            {
                AnsiConsole.MarkupLine($"[green]Filamento deshabilitado con éxito! ID: {resultado.Id}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error al deshabilitar el filamento.[/]");
            }
        }
    }
} 