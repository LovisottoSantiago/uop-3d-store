using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System.Collections.Generic;

namespace Part1.ConsoleApp.Application.Queries.OrdenDeCompraQueries.Get
{
    public class GetAllOrdenesDeCompraQuery : IRequest<List<OrdenDeCompra>>
    {
    }
} 