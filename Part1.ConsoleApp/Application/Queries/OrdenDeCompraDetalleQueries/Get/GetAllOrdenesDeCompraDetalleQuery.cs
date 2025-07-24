using MediatR;
using Part1.ConsoleApp.Domain.Entities;
using System.Collections.Generic;

namespace Part1.ConsoleApp.Application.Queries.OrdenDeCompraDetalleQueries.Get
{
    public class GetAllOrdenesDeCompraDetalleQuery : IRequest<List<OrdenDeCompraDetalle>>
    {
    }
} 