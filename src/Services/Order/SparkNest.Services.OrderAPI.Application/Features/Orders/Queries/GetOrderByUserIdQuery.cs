using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Queries
{
    public class GetOrderByUserIdQuery : IRequest<Response<List<OrderDTO>>>
    {
        public string UserId { get; set; }
    }
}
