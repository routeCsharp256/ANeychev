using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.Infrastructure.Middlewares
{
    public sealed class LiveMiddleware
    {
        public LiveMiddleware(RequestDelegate next)
        {
        }
        
        public async Task InvokeAsync(HttpContext context) => await context.Response.WriteAsync(string.Empty);
    }
}