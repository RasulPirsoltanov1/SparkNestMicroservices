using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.Mapping
{
    public static class ObjectMapping
    {
        private static Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var cfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });
            return cfg.CreateMapper();
        });

        public static IMapper Mapper => _lazy.Value;
    }
}
