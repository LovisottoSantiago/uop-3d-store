using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.OrdenDeCompraCommands.Update
{
    public class UpdateOrdenDeCompraCommandHandler : IRequestHandler<UpdateOrdenDeCompraCommand, OrdenDeCompra>
    {
        private readonly AppDbContext _context;
        public UpdateOrdenDeCompraCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrdenDeCompra> Handle(UpdateOrdenDeCompraCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implementar lógica de actualización
            return null;
        }
    }
} 