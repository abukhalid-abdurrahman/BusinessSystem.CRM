using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessSystem.CRM.Middlewares
{
    public class MobileRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        public MobileRedirectMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(context.Request.Headers.ContainsKey("x-mobile"))
            {
                context.Response.Redirect("/Home/HandleError/302");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
