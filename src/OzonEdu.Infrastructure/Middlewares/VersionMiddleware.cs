using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.Infrastructure.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var version = new 
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version",
                SeviceName = Assembly.GetEntryAssembly()?.GetName().Name ?? "no name"
            };
            await context.Response.WriteAsJsonAsync(version);
        }
    }
}