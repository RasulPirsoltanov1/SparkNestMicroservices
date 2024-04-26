using SparkNest.Services.BlogAPI.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Domain.Entities
{
    public class Subscriber : BaseEntity
    {
        public string? Email { get; set; }
    }
}
