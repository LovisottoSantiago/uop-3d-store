using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.MarcaCommands.Delete
{
    public class DeleteMarcaCommandHandler : IRequestHandler<DeleteMarcaCommand, Marca>
    {
        private readonly AppDbContext _context;
        public DeleteMarcaCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Marca> Handle(DeleteMarcaCommand request, CancellationToken cancellationToken)
        {
            var marca = _context.Marcas.FirstOrDefault(m => m.Id == request.Id);

            if (marca == null)
            {
                return default;
            }

            _context.Remove(marca);
            await _context.SaveChangesAsync();
            return marca;
        }
    }
} 