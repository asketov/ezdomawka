using BLL.Services;
using Common.Consts;
using Common.Generics;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezdomawka.Middlewares.BanMiddleware
{
    public class BanMiddleware
    {
        private readonly RequestDelegate _next;

        public BanMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task InvokeAsync(HttpContext context, UserService userService)
        {
            if (!context.User.Identity!.IsAuthenticated) await _next(context);
            else
            {
                var userId = context.User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim);
                if (await userService.UserIsBanned(userId))
                {
                    //var routeData = context.GetRouteData();
                    //routeData.Values["controller"] = "User";
                    //routeData.Values["action"] = "IntroduceBan";
                    context.Response.Redirect("/user/introduceban", true);
                }
                //await _next(context);
            }
        }
    }
}
