using MediatR;
using SparkNest.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Features.Categorys.Commands.Delete
{
    public class CategoryDeleteCommandRequest : IRequest<Response<bool>>
    {
        public string CategoryId { get; set; }
    }
}
