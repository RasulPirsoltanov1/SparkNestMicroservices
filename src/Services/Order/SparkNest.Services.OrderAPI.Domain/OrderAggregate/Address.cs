using SparkNest.Services.OrderAPI.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Domain.OrderAggregate
{
    public class Address : ValueObject
    {
        public Address(string province, string street, string district, string line, string zipCode)
        {
            Province = province;
            Street = street;
            District = district;
            Line = line;
            ZipCode = zipCode;
        }

        public string Province { get; private set; }
        public string Street { get; private set; }
        public string District { get; private set; }
        public string Line { get; private set; }
        public string ZipCode { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province; yield return Street; yield return District; yield return Line;
        }
    }
}
