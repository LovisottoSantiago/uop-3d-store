using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Delete
{
    public class DeleteOrdenDeCompraCommandHandler : IRequestHandler<DeleteOrdenDeCompraCommand, OrdenDeCompra>
    {
        private readonly AppDbContext _context;
        public DeleteOrdenDeCompraCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompra> Handle(DeleteOrdenDeCompraCommand request, CancellationToken cancellationToken)
        {
            var ordenDeCompra = _context.OrdenDeCompras.FirstOrDefault(o => o.Id == request.Id);

            if (ordenDeCompra == null)
            {
                return default;
            }

            _context.Remove(ordenDeCompra);
            await _context.SaveChangesAsync();
            return ordenDeCompra;
        }
    }
} 