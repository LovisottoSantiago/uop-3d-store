using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Part1.ConsoleApp.Application.Commands.FilamentoCommands.Create;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using Part1.ConsoleApp.Menu;

namespace Part1.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            // Configurar DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var connectionString = config.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Registrar MediatR 
            services.AddMediatR(typeof(CreateFilamentoCommandHandler).Assembly);

            var provider = services.BuildServiceProvider();
            var dbContext = provider.GetRequiredService<AppDbContext>();
            var mediator = provider.GetRequiredService<IMediator>();

            if (TestDatabaseConnection(dbContext))
            {
                await ConsoleMenu.MostrarMenu(mediator, dbContext);
            }
        }

        static bool TestDatabaseConnection(AppDbContext context)
        {
            try
            {
                context.Database.OpenConnection();
                Console.WriteLine("Conectado a la base de datos exitosamente.");
                context.Database.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
