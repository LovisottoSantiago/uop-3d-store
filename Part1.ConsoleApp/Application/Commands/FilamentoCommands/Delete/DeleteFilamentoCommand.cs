using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.ConsoleApp.Application.Commands.FilamentoCommands.Delete
{
    public class DeleteFilamentoCommand : IRequest<Filamento>
    {
        public int Id { get; set; }
    }
}
