using Microsoft.Extensions.DependencyInjection;
using SparkNest.UI.MVC.Application.Abstractions;
using SparkNest.UI.MVC.Infrastructure.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application
{
    public static class Registration
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
           

            return services;
        }
    }
}
