using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.AutoMapper.Profiles;
using BLL.Services;

namespace BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection
            services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<AuthService>();
            services.AddScoped<AdminService>();
            services.AddAutoMapper(typeof(UserProfile).Assembly, typeof(AuthProfile).Assembly, typeof(AdminProfile).Assembly);
            return services;
        }
    }
}
