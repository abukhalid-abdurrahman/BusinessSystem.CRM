using BusinessSystem.CRM.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;

namespace BusinessSystem.CRM.Extensions
{
    public static class MobileRedirectExtension
    {
        public static IApplicationBuilder UseMobileRedierct(this IApplicationBuilder builder)
        {
            if(builder == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                return builder.UseMiddleware<MobileRedirectMiddleware>();
            }
        }
    }
}
