using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using Part1.ConsoleApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.FilamentoCommands.Delete
{
    public class DeleteFilamentoCommandHandler : IRequestHandler<DeleteFilamentoCommand, Filamento>
    {
        private readonly AppDbContext _context;

        public DeleteFilamentoCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Filamento?> Handle(DeleteFilamentoCommand request, CancellationToken cancellationToken)
        {
            var filamento = _context.Filamentos.FirstOrDefault(f => f.Id == request.Id);

            if (filamento == null)
            {
                return default;
            }

            _context.Filamentos.Remove(filamento);
            await _context.SaveChangesAsync();
            return filamento;
        }


    }
}
