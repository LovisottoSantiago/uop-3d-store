using MediatR;
using System.Collections.Generic;
using Part1.ConsoleApp.Domain.Entities;

namespace Part1.ConsoleApp.Application.Queries.EstadoQueries.Get
{
    public class GetAllEstadosQuery : IRequest<List<Estado>>
    {
    }
} 