using MediatR;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Menu
{
    public static class MarcaMenu
    {
        public static async Task MostrarSubMenu(IMediator mediator, AppDbContext dbContext)
        {
            Console.WriteLine("[yellow]Submenú de Marcas (por implementar)...[/]");
            await Task.Delay(1000);
        }
    }
} 