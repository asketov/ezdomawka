using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection
            services)
        {
            //services.AddScoped();
            return services;
        }
    }
}
