using Amazon.Runtime.Internal;
using MediatR;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.DTOs;
using SparkNest.Services.BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Application.Features.Categorys.Queries.GetAll
{
    public class CategoryGetAllQueryRequest:IRequest<Response<List<CategoryDTO>>>
    {
    }
}
