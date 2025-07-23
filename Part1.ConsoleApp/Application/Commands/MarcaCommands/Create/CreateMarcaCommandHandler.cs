using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.MarcaCommands.Create
{
    public class CreateMarcaCommandHandler : IRequestHandler<CreateMarcaCommand, Marca>
    {
        private readonly AppDbContext _context;
        public CreateMarcaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Marca> Handle(CreateMarcaCommand request, CancellationToken cancellationToken)
        {
            var marca = new Marca
            {
                Nombre = request.Nombre,
                DistribuidorId = request.DistribuidorId
            };
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return marca;
        }
    }
} 