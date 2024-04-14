using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Queries.GetAll
{
    public class GetAllOrdersQuery : IRequest<Response<List<OrderDTO>>>
    {
    }
}
