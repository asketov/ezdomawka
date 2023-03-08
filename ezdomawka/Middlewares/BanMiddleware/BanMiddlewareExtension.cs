using BLL.Services;
using Common.Exceptions.User;
using Common.Generics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Consts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ezdomawka.Middlewares.BanMiddleware
{
    public static class BanMiddlewareExtension
    {
        public static IApplicationBuilder UseBanMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BanMiddleware>();
        }
    }
}
