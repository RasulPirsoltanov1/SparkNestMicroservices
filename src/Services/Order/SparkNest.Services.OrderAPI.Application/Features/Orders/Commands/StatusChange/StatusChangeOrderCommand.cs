using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.OrderAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.Features.Orders.Commands.StatusChange
{
    public class StatusChangeOrderCommand : IRequest<Response<bool>>
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
    }
}
