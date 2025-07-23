using MediatR;
using Part1.ConsoleApp.Application.Commands.FilamentoCommands.Delete;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.InsumoCommands.Delete
{
    public class DeleteInsumoCommandHandler : IRequestHandler<DeleteInsumoCommand, Insumo>
    {
        private readonly AppDbContext _context;

        public DeleteInsumoCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Insumo> Handle(DeleteInsumoCommand request, CancellationToken cancellationToken)
        {
            var insumo = _context.Insumos.FirstOrDefault(ins => ins.Id == request.Id);

            if (insumo == null)
            {
                return default;
            }

            _context.Remove(insumo);
            await _context.SaveChangesAsync();
            return insumo;
        }
    }
}
