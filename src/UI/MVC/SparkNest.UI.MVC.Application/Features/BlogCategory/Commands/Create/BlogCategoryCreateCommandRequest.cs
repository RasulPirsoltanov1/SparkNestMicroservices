using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.Features.BlogCategory.Commands.Create
{
    public class BlogCategoryCreateCommandRequest : IRequest<bool>
    {
        [Required]
        public string Name { get; set; }
    }
}
