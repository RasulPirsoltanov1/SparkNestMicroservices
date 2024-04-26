using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SparkNest.UI.MVC.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Infrastructure
{
    public static class Register
    {
        public static IServiceCollection AddMvcInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Data Source=localhost,1436;Database=MessageDB;User Id=sa;Password=Rasul123.;TrustServerCertificate=true;");
            });

            return services;
        }
    }
}
