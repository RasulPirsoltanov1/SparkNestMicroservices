using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.DTOs
{
    public class AddressDTO
    {
        public string Province { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Line { get; set; }
        public string ZipCode { get; set; }
    }
}
